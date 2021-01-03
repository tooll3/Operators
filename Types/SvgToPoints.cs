using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Numerics;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
//using SharpDX;

using Svg;
using Svg.Pathing;
//using System.Drawing;
using T3.Core.Logging;
using Point = T3.Core.DataTypes.Point;

// using Device = SharpDX.Direct3D11.Device;
// using Point = T3.Core.DataTypes.Point;

namespace T3.Operators.Types.Id_e8d94dd7_eb54_42fe_a7b1_b43543dd457e
{
    public class SvgToPoints : Instance<SvgToPoints>
    {
        [Output(Guid = "e21e3843-7d63-4db2-9234-77664e872a0f")]
        public readonly Slot<StructuredList> ResultList = new Slot<StructuredList>();

        public SvgToPoints()
        {
            ResultList.UpdateAction = Update;
            _pointListWithSeparator.TypedElements[_pointListWithSeparator.NumElements - 1] = Point.Separator();
        }

        private struct Command
        {
            private char Code;
            private int NumberCount;
        }
        
        private void Update(EvaluationContext context)
        {
            var filepath = FilePath.GetValue(context);
            if (!File.Exists(filepath))
                return;            


            // see http://vvvv.github.io/SVG/doc/Q&A.html#how-to-render-an-svg-image-to-a-single-color-bitmap-image
            var svgDoc = SvgDocument.Open<SvgDocument>(filepath, null);

            GraphicsPath newPath = new GraphicsPath();

            var paths = new List<GraphicsPath>();
            ConvertAllNodesIntoGraphicPaths(svgDoc.Descendants(), paths);
            newPath.Flatten();

            // var pointCount = newPath.PathPoints.Length;

            var totalPointCount = 0;
            foreach (var p in paths)
            {
                p.Flatten();
                totalPointCount += p.PointCount + 1;
            }
            
            if (totalPointCount != _pointListWithSeparator.NumElements )
            {
                _pointListWithSeparator.SetLength(totalPointCount);
            }

            int pointIndex = 0;
            foreach (var path in paths)
            {
                foreach (var s in path.PathPoints)
                {
                    Log.Debug(s.ToString());
                    _pointListWithSeparator.TypedElements[pointIndex].Position = new Vector3(s.X, 1 -s.Y, 0) * 0.1f;
                    _pointListWithSeparator.TypedElements[pointIndex].W = 1;
                    _pointListWithSeparator.TypedElements[pointIndex].Orientation = Quaternion.Identity;
                    pointIndex++;
                }

                _pointListWithSeparator.TypedElements[pointIndex] = Point.Separator();
                pointIndex++;
            }

            ResultList.Value = _pointListWithSeparator;
        }

        private void ConvertAllNodesIntoGraphicPaths(IEnumerable<SvgElement> nodes, List<GraphicsPath> paths)
        {
            GraphicsPath path = null;
            foreach (var node in nodes)
            {
                if (!(node is SvgPath svgPath))
                    continue;
                
                //Log.Debug($"NODE:{svgPath} pathLength:{svgPath.PathLength}");
                foreach (var s in svgPath.PathData)
                {
                    if (s is SvgMoveToSegment
                        || s is SvgClosePathSegment)
                    {
                        if(path != null)
                            paths.Add(path);
                        path = null;
                    }
                    else
                    {
                        CreateOrAppendToPath(s);
                    }
                }

                if (path != null)
                {
                    Log.Warning("Unclosed svg path?");
                }
            }

            void CreateOrAppendToPath(SvgPathSegment s)
            {
                if (path == null)
                    path = new GraphicsPath();

                s.AddToPath(path);
            }
        }
        
        
        
        
        
        private readonly StructuredList<Point> _pointListWithSeparator = new StructuredList<Point>(101);

        [Input(Guid = "EF2A461D-C66D-44D8-8B0E-E48A57EC991F")]
        public readonly InputSlot<string> FilePath = new InputSlot<string>();
    }
}