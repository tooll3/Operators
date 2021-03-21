using System;
using System.Collections.Generic;
using System.Resources;
using Microsoft.Win32;
using SharpDX;
using T3.Core;
using T3.Core.Animation;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using T3.Operators.Types.Id_8211249d_7a26_4ad0_8d84_56da72a5c536;
using Point = T3.Core.DataTypes.Point;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;
using Vector4 = SharpDX.Vector4;

namespace T3.Operators.Types.Id_b7345438_f3f4_4ad3_9c57_6076ed0e9399
{
    public class PrepareSlidingLineTransition : Instance<PrepareSlidingLineTransition>
    {
        [Output(Guid = "adcfd192-23a3-48c1-ae21-e7d36e055673")]
        public readonly Slot<StructuredList> ResultList = new Slot<StructuredList>();

        [Output(Guid = "4E6983BB-482F-48E7-93B1-73C73F43C60A")]
        public readonly Slot<int> StrokeCount = new Slot<int>();

        public PrepareSlidingLineTransition()
        {
            ResultList.UpdateAction = Update;
        }

        private static List<Segment> segments = new List<Segment>(1000);

        private struct Segment
        {
            public int PointIndex;
            public float PointCount;
            public float AccumulatedLength;
            public float SegmentLength;
        }



        private void Update(EvaluationContext context)
        {
            if (!(SourcePoints.GetValue(context) is StructuredList<Point> sourcePoints))
            {
                return;
            }

            if (sourcePoints.NumElements == 0)
            {
                sourcePoints.SetLength(0);
                ResultList.Value = sourcePoints;
                return;
            }

            var spread = Spread.GetValue(context);
            var spreadMode = (SpreadModes)SpreadMode.GetValue(context);

            var indexWithinSegment = 0;
            var lineSegmentLength = 0f;
            var totalLength = 0f;
            var maxLength = float.NegativeInfinity;

            //Log.Debug("here");

            // Measure...
            segments.Clear();
            for (var pointIndex = 0; pointIndex < sourcePoints.NumElements; pointIndex++)
            {
                if (float.IsNaN(sourcePoints.TypedElements[pointIndex].W))
                {
                    var hasAtLeastTwoPoints = indexWithinSegment > 1;
                    if (hasAtLeastTwoPoints)
                    {
                        if (lineSegmentLength > maxLength)
                            maxLength = lineSegmentLength;
                        
                        totalLength += lineSegmentLength;
                        segments.Add(new Segment
                                         {
                                             PointIndex = pointIndex - indexWithinSegment,
                                             PointCount = indexWithinSegment,
                                             AccumulatedLength = totalLength,
                                             SegmentLength = lineSegmentLength
                                         });
                    }

                    lineSegmentLength = 0;
                    indexWithinSegment = 0;
                }
                else
                {
                    if (indexWithinSegment > 0)
                    {
                        lineSegmentLength += Vector3.Distance(sourcePoints.TypedElements[pointIndex - 1].Position,
                                                              sourcePoints.TypedElements[pointIndex].Position);
                    }

                    indexWithinSegment++;
                }
            }

            //var normalizeFactor = sp 
            if (totalLength < 0.0001f || segments.Count < 2)
            {
                Log.Warning("Stroke animation requires at least two segments with of some length");
                return;
            }

            // Write offsets...
            float dist = maxLength / (segments.Count - 1);
            
            for (var segmentIndex = 0; segmentIndex < segments.Count; segmentIndex++)
            {
                var segmentOffset = ComputeOverlappingProgress(0, segmentIndex, segments.Count, spread);
                var lengthProgressWithingSegment = 0f;
                var segment = segments[segmentIndex];

                // see https://www.figma.com/file/V5k13NMMIsnAnbWH651clI/Untitled?node-id=205%3A96
                var stackedRange = TimeRange.FromStartAndDuration( segment.AccumulatedLength - segment.SegmentLength, segment.SegmentLength) * (1/totalLength);
                
                var anchor = segmentIndex * segment.SegmentLength / (segments.Count - 1);
                var pGrid = segmentIndex * dist;
                var packedRange = TimeRange.FromStartAndDuration(pGrid - anchor, segment.SegmentLength) * (1/maxLength);
                var range = TimeRange.Lerp(packedRange, stackedRange, spread);
                
                for (var pointIndexInSegment = 0; pointIndexInSegment < segment.PointCount; pointIndexInSegment++)
                {
                    var pi = segment.PointIndex + pointIndexInSegment;
                    if (pointIndexInSegment > 0)
                    {
                        lengthProgressWithingSegment += Vector3.Distance(sourcePoints.TypedElements[pi - 1].Position,
                                                                         sourcePoints.TypedElements[pi].Position);
                    }

                    //var normalizedSegmentPosition = lengthProgressWithingSegment / segment.SegmentLength.Clamp(0.001f, 999999f);
                    var normalizedSegmentPosition = (float)pointIndexInSegment / (segment.PointCount -1);
                    // float t= -segmentOffset *0.2f + pointIndexInSegment / segment.PointCount / 2;
                    float w=0;
                    switch (spreadMode)
                    {
                        case SpreadModes.IgnoreStrokeLengths:
                            float f= lengthProgressWithingSegment / segment.SegmentLength.Clamp(0.001f,999999f);
                            w = (f - segmentOffset) / (segments.Count + 1); // + spread/ spread.Clamp(0.001f, 9999f); // / segments.Count
                            break;
                        
                        case SpreadModes.UseStrokeLength:
                            w  = MathUtils.Lerp(range.Start, range.End, normalizedSegmentPosition);
                            //w = MathUtils.Lerp(0f, 1f, normalizedSegmentPosition);
                            break;
                        
                        case SpreadModes.Weird:
                            w = segmentOffset * 0.2f + pointIndexInSegment / segment.PointCount / 2;
                            break;
                        
                    }

                    sourcePoints.TypedElements[pi].W = w;
                }
            }

            StrokeCount.Value = segments.Count;
            ResultList.Value = sourcePoints;
            StrokeCount.DirtyFlag.Clear();
            ResultList.DirtyFlag.Clear();
        }

        
        /// <summary>
        /// Computes the sub-progress for elements of an animation that's build of multiple delayed
        /// animations. Progress values always normalized.
        /// </summary>
        /// <param name="spread">Controls how much the sub-animations should overlay.
        /// 0 means that all animation are played simultaneously.
        /// 1 means they are played one after another.
        /// Larger spreads adds a pause between animations</param>
        private static float ComputeOverlappingProgress(float normalizedProgress, int index, int count, float spread)
        {
            var N = (spread * count - spread + 1);
            var partialLength = 1 / N;
            var offset = index * spread / N;
            var progress = (normalizedProgress - offset) / partialLength;
            return progress;
        }
        
        [Input(Guid = "5FD5EEA5-B7AB-406F-8C10-8435D59297B5")]
        public readonly InputSlot<float> Spread = new InputSlot<float>();

        [Input(Guid = "C1FA1A4E-8884-4A6F-AC80-F22D3B5DFE2F", MappedType = typeof(SpreadModes))]
        public readonly InputSlot<int> SpreadMode = new InputSlot<int>();

        [Input(Guid = "82b2e8d3-40c2-4a4c-a9ad-806d5097a8fd")]
        public readonly InputSlot<StructuredList> SourcePoints = new InputSlot<StructuredList>();

        private enum SpreadModes
        {
            IgnoreStrokeLengths,
            UseStrokeLength,
            Weird,
        }
    }
}