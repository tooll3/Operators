using System;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using T3.Core.Rendering;
using Buffer = SharpDX.Direct3D11.Buffer;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace T3.Operators.Types.Id_5777a005_bbae_48d6_b633_5e998ca76c91
{
    public class CylinderMesh : Instance<CylinderMesh>
    {
        [Output(Guid = "b4bed6e3-bef5-4601-99bd-f85bf1a956f5")]
        public readonly Slot<MeshBuffers> Data = new Slot<MeshBuffers>();

        public CylinderMesh()
        {
            Data.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            try
            {
                var resourceManager = ResourceManager.Instance();

                var lowerRadius = Radius.GetValue(context);
                var upperRadius = lowerRadius + RadiusOffset.GetValue(context);
                var height = Height.GetValue(context);

                var rows = Rows.GetValue(context).Clamp(1, 10000);
                var columns = Columns.GetValue(context).Clamp(1, 10000);

                var c = Center.GetValue(context);
                var center = new SharpDX.Vector3(c.X, c.Y, c.Z);


                var spinInRad = Spin.GetValue(context) * MathUtils.ToRad;
                var twistInRad = Twist.GetValue(context) * MathUtils.ToRad;
                var basePivot = BasePivot.GetValue(context); 
                
                var fillRatio = Fill.GetValue(context) / 360f;
                var capSegments = CapSegments.GetValue(context).Clamp(0, 1000);
                var addCaps = capSegments > 0;
                var isHullClosed = false; //Math.Abs(fillRatio - 1) < 0.01f;

                var isFlipped = lowerRadius < 0;
                
                var vertexHullColumns = isHullClosed ? columns : columns + 1;

                var hullTriangleCount = addCaps
                                            ? rows * columns * 2 + 2 * (capSegments - 1 * columns * 2 + columns)
                                            : rows * columns * 2;

                var hullVerticesCount = addCaps
                                            ? (rows + 1) * vertexHullColumns + 2 * (1 + capSegments * vertexHullColumns)
                                            : (rows + 1) * vertexHullColumns;

                // Create buffers
                if (_vertexBufferData.Length != hullVerticesCount)
                    _vertexBufferData = new PbrVertex[hullVerticesCount];

                if (_indexBufferData.Length != hullTriangleCount)
                    _indexBufferData = new SharpDX.Int3[hullTriangleCount];

                // Initialize
                var radiusAngleFraction = fillRatio / (vertexHullColumns - 1) * 2.0 * Math.PI;
                var rowStep = height / rows;

                for (var rowIndex = 0; rowIndex < rows + 1; ++rowIndex)
                {
                    var heightFraction = rowIndex * rowStep;
                    var rowRadius = MathUtils.Lerp(lowerRadius, upperRadius, heightFraction);
                    var rowLevel = height * (heightFraction - basePivot);
                    var rowCenter = new SharpDX.Vector3(center.X, rowLevel, center.Z);

                    for (var columnIndex = 0; columnIndex < vertexHullColumns; ++columnIndex)
                    {
                        var columnAngle = columnIndex * radiusAngleFraction + spinInRad + twistInRad * heightFraction + Math.PI;
                        
                        var u0 = columnIndex / (float)columns;
                        var u1 = (columnIndex + 1) / (float)rows;

                        var v0 = (rowIndex + 1) / (float)rows;
                        var v1 = rowIndex / (float)rows;

                        var p = new SharpDX.Vector3((float)Math.Sin(columnAngle) * rowRadius,
                                                    rowLevel,
                                                    (float)Math.Cos(columnAngle) * rowRadius);

                        var p1 = new SharpDX.Vector3((float)Math.Sin(columnAngle) * rowRadius,
                                                     rowLevel + rowStep,
                                                     (float)Math.Cos(columnAngle) * rowRadius
                                                     );

                        var p2 = new SharpDX.Vector3((float)Math.Sin(columnAngle + radiusAngleFraction) * rowRadius,
                                                     rowLevel,
                                                     (float)Math.Cos(columnAngle + radiusAngleFraction) * rowRadius
                                                     );

                        // var p1 = new SharpDX.Vector3((float)(Math.Sin(radiusAngle + radiusAngleFraction) * (tubePosition1X + lowerRadius)),
                        //                              (float)(Math.Cos(radiusAngle + radiusAngleFraction) * (tubePosition1X + lowerRadius)),
                        //                              (float)tubePosition1Y);
                        //
                        // var p2 = new SharpDX.Vector3((float)(Math.Sin(radiusAngle) * (tubePosition2X + lowerRadius)),
                        //                              (float)(Math.Cos(radiusAngle) * (tubePosition2X + lowerRadius)),
                        //                              (float)tubePosition2Y);

                        var uv0 = new SharpDX.Vector2(u0, v1);
                        var uv1 = new SharpDX.Vector2(u1, v1);
                        var uv2 = new SharpDX.Vector2(u1, v0);

                        //var tubeCenter1 = new SharpDX.Vector3((float)Math.Sin(radiusAngle), (float)Math.Cos(radiusAngle), 0.0f) * lowerRadius;
                        var normal0 = SharpDX.Vector3.Normalize(p - rowCenter);

                        MeshUtils.CalcTBNSpace(p, uv0, p1, uv1, p2, uv2, normal0, out var tangent0, out var binormal0);
                        

                        var vertexIndex = rowIndex * vertexHullColumns + columnIndex;
                        _vertexBufferData[vertexIndex ] = new PbrVertex
                                                                 {
                                                                     Position = p + center,
                                                                     Normal = isFlipped ? normal0 * -1 : normal0,
                                                                     Tangent = tangent0,
                                                                     Bitangent = isFlipped ? binormal0 * -1 : binormal0,
                                                                     Texcoord = uv0
                                                                 };

                        if (columnIndex >= vertexHullColumns - 1 || rowIndex >= rows  )
                            continue;

                        var faceIndex = 2 * (rowIndex * (vertexHullColumns - 1)  + columnIndex);
                        if (isFlipped)
                        {
                            _indexBufferData[faceIndex + 0] = new SharpDX.Int3(vertexIndex + vertexHullColumns, vertexIndex + 1, vertexIndex + 0);
                            _indexBufferData[faceIndex + 1] = new SharpDX.Int3(vertexIndex + vertexHullColumns + 1, vertexIndex + 1, vertexIndex + vertexHullColumns);
                        }
                        else
                        {
                            _indexBufferData[faceIndex + 0] = new SharpDX.Int3(vertexIndex + 0, vertexIndex + 1, vertexIndex + vertexHullColumns);
                            _indexBufferData[faceIndex + 1] = new SharpDX.Int3(vertexIndex + vertexHullColumns, vertexIndex + 1, vertexIndex + vertexHullColumns + 1);
                        }
                    }
                }

                // Write Data
                _vertexBufferWithViews.Buffer = _vertexBuffer;
                resourceManager.SetupStructuredBuffer(_vertexBufferData, PbrVertex.Stride * hullVerticesCount, PbrVertex.Stride, ref _vertexBuffer);
                resourceManager.CreateStructuredBufferSrv(_vertexBuffer, ref _vertexBufferWithViews.Srv);
                resourceManager.CreateStructuredBufferUav(_vertexBuffer, UnorderedAccessViewBufferFlags.None, ref _vertexBufferWithViews.Uav);

                _indexBufferWithViews.Buffer = _indexBuffer;
                const int stride = 3 * 4;
                resourceManager.SetupStructuredBuffer(_indexBufferData, stride * hullTriangleCount, stride, ref _indexBuffer);
                resourceManager.CreateStructuredBufferSrv(_indexBuffer, ref _indexBufferWithViews.Srv);
                resourceManager.CreateStructuredBufferUav(_indexBuffer, UnorderedAccessViewBufferFlags.None, ref _indexBufferWithViews.Uav);

                _data.VertexBuffer = _vertexBufferWithViews;
                _data.IndicesBuffer = _indexBufferWithViews;
                Data.Value = _data;
                Data.DirtyFlag.Clear();
            }
            catch (Exception e)
            {
                Log.Error("Failed to create torus mesh:" + e.Message);
            }
        }

        private Buffer _vertexBuffer;
        private PbrVertex[] _vertexBufferData = new PbrVertex[0];
        private readonly BufferWithViews _vertexBufferWithViews = new BufferWithViews();

        private Buffer _indexBuffer;
        private SharpDX.Int3[] _indexBufferData = new SharpDX.Int3[0];
        private readonly BufferWithViews _indexBufferWithViews = new BufferWithViews();

        private readonly MeshBuffers _data = new MeshBuffers();

        [Input(Guid = "66332A91-E0C2-442A-99F6-347DEDAED72E")]
        public readonly InputSlot<Vector3> Center = new InputSlot<Vector3>();

        [Input(Guid = "8d290afb-2574-4afa-a545-a0d3588f89f6")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "4C91B66C-670D-45FF-94CC-01D1A68CD040")]
        public readonly InputSlot<float> RadiusOffset = new InputSlot<float>();

        [Input(Guid = "57f3310c-6ed2-4a52-af72-43e083f73360")]
        public readonly InputSlot<float> Height = new InputSlot<float>();

        [Input(Guid = "4DD4C4F0-C6B7-4EE8-92E2-CB8DF6131E0A")]
        public readonly InputSlot<int> Rows = new InputSlot<int>();

        [Input(Guid = "321693A5-4E2C-47A0-A42E-95CBDC6EBF80")]
        public readonly InputSlot<int> Columns = new InputSlot<int>();

        [Input(Guid = "C29B5881-85BC-4D29-BC72-6DD36730FA8F")]
        public readonly InputSlot<float> Spin = new InputSlot<float>();

        [Input(Guid = "1D1CE8C4-FD3C-4D69-BE0E-679247A811C9")]
        public readonly InputSlot<float> Twist = new InputSlot<float>();

        [Input(Guid = "91FD4FBF-1CEC-4D89-8014-CEED0021A5EE")]
        public readonly InputSlot<float> Fill = new InputSlot<float>();

        [Input(Guid = "6DDF5966-9140-4BEA-A56B-20690F9F436F")]
        public readonly InputSlot<float> BasePivot = new InputSlot<float>();


        [Input(Guid = "DB5E3C51-5765-44D8-A61B-A7B552FCE5B3")]
        public readonly InputSlot<int> CapSegments = new InputSlot<int>();
    }
}