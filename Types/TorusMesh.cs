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

namespace T3.Operators.Types.Id_a835ab86_29c1_438e_a7f7_2e297108bfd5
{
    public class TorusMesh : Instance<TorusMesh>
    {
        [Output(Guid = "f8f17f87-56f2-4411-b9bf-b9193b9aa90d")]
        public readonly Slot<MeshBuffers> Data = new Slot<MeshBuffers>();

        public TorusMesh()
        {
            Data.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            try
            {
                var resourceManager = ResourceManager.Instance();

                var radiusMajor = Radius.GetValue(context);
                var radiusMinor = Thickness.GetValue(context);

                var segments = Segments.GetValue(context);
                var tessX = segments.Height.Clamp(1, 10000) + 1;
                var tessY = segments.Width.Clamp(1, 10000) + 1;

                var spin = Spin.GetValue(context);
                var spinMajorInRad = spin.X * MathUtils.ToRad;
                var spinMinorInRad = spin.Y * MathUtils.ToRad;

                var portion = Portion.GetValue(context);
                var portionMajor = portion.X;
                var portionMinor = portion.Y;

                var smoothAngle = SmoothAngle.GetValue(context);
                
                var useFlatShading = portionMajor / tessX > smoothAngle / 360 || portionMinor / tessY > smoothAngle / 360;
                var faceCount = (tessX -1)  * (tessY - 1) * 2;
                var verticesCount = tessX * tessY;

                // Create buffers
                if (_vertexBufferData.Length != verticesCount)
                    _vertexBufferData = new PbrVertex[verticesCount];
                
                if (_indexBufferData.Length != faceCount)
                    _indexBufferData = new SharpDX.Int3[faceCount];


                // Initialize
                var majorAngleFraction = portionMinor / (tessX -1) * 2.0 * Math.PI;
                var secondaryAngleFraction = portionMajor / (tessY -1) * 2.0 * Math.PI;

                for (int x = 0; x < tessX; ++x)
                {
                    var tubeAngle = x * majorAngleFraction + spinMinorInRad;
                    var posOnRadiusX = Math.Sin(tubeAngle) * radiusMinor;
                    var posOnRadiusY = Math.Cos(tubeAngle) * radiusMinor;
                    
                    double tubePosition1Y = Math.Cos(tubeAngle)*radiusMinor;
                    double tubePosition1X = Math.Sin(tubeAngle)*radiusMinor;
                    // double tubePosition2Y = Math.Cos(tubeAngle + tubeAngleFraction)*radiusMinor;
                    // double tubePosition2X = Math.Sin(tubeAngle + tubeAngleFraction)*radiusMinor;                    
                    //var posOnRadius = new Vector2((float)Math.Sin(tubeAngle) * radiusMinor, (float)Math.Cos(tubeAngle) * radiusMinor);

                    var v0 = x / (float)tessX;
                    var v1 = (x + 1) / (float)tessX;

                    for (int y = 0; y < tessY; ++y)
                    {
                        var vertexIndex = y + x * tessY;
                        var faceIndex =  2 * (y + x * (tessY-1));
                        
                        var u0 = (y + 1) / (float)tessX;
                        var u1 = y / (float)tessX;

                        var secondaryAngle = y * secondaryAngleFraction + spinMajorInRad;

                        // var p0 = new Vector3((float) (Math.Sin(axisAngle + axisAngleFraction)*(tubePosition2X + radiusMajor)),
                        //                      (float) (Math.Cos(axisAngle + axisAngleFraction)*(tubePosition2X + radiusMajor)), (float) tubePosition2Y);
                        // var p1 = new Vector3((float) (Math.Sin(axisAngle)*(tubePosition2X + radiusMajor)),
                        //                      (float) (Math.Cos(axisAngle)*(tubePosition2X + radiusMajor)), (float) tubePosition2Y);
                        // var p2 = new Vector3((float) (Math.Sin(axisAngle)*(tubePosition1X + radiusMajor)),
                        //                      (float) (Math.Cos(axisAngle)*(tubePosition1X + radiusMajor)), (float) tubePosition1Y);
                        
                        var p0 = new SharpDX.Vector3((float)(Math.Sin(secondaryAngle) * (posOnRadiusX + radiusMajor)),
                                                     (float)(Math.Cos(secondaryAngle) * (posOnRadiusX + radiusMajor)), 
                                                     (float)posOnRadiusY);
                        
                        var p1 = new SharpDX.Vector3((float)(Math.Sin(secondaryAngle + secondaryAngleFraction) * (posOnRadiusX + radiusMajor)),
                                                     (float)(Math.Cos(secondaryAngle + secondaryAngleFraction) * (posOnRadiusX + radiusMajor)), 
                                                     (float)posOnRadiusY);
                        
                        var p2 = new SharpDX.Vector3((float)(Math.Sin(secondaryAngle + secondaryAngleFraction) * (posOnRadiusX + radiusMajor)),
                                                     (float)(Math.Cos(secondaryAngle + secondaryAngleFraction) * (posOnRadiusX + radiusMajor)), 
                                                     (float)posOnRadiusY);
                        
                        var uv0 = new SharpDX.Vector2(u0, v1);
                        var uv1 = new SharpDX.Vector2(u1, v1);
                        var uv2 = new SharpDX.Vector2(u1, v0);

                        var tubeCenter1 = new SharpDX.Vector3((float)Math.Sin(secondaryAngle), (float)Math.Cos(secondaryAngle), 0.0f) * radiusMajor;
                        var normal0 = SharpDX.Vector3.Normalize(useFlatShading 
                                                                    ? SharpDX.Vector3.Cross(p0 - p1, p0 - p2) 
                                                                    : p0 - tubeCenter1);
                        
                        //Log.Debug($" {p0}   {p1}  {p2}    N0 {normal0}");

                        MeshUtils.CalcTBNSpace(p0, uv0, p1, uv1, p2, uv2, normal0, out var tangent0, out var binormal0);

                        _vertexBufferData[vertexIndex + 0] = new PbrVertex
                                                            {
                                                                Position = p0,
                                                                Normal = normal0,
                                                                Tangent = tangent0,
                                                                Bitangent = binormal0,
                                                                Texcoord = uv0
                                                            };

                        if (x >= tessX - 1 || y >= tessY - 1)
                            continue;
                        
                        _indexBufferData[faceIndex + 0] = new SharpDX.Int3(vertexIndex + 0, vertexIndex + 1, vertexIndex + tessY);
                        _indexBufferData[faceIndex + 1] = new SharpDX.Int3(vertexIndex + tessY , vertexIndex + 1, vertexIndex + tessY+1);
                    }
                }
                
                // Write Data
                _vertexBufferWithViews.Buffer = _vertexBuffer;
                resourceManager.SetupStructuredBuffer(_vertexBufferData, PbrVertex.Stride * verticesCount, PbrVertex.Stride, ref _vertexBuffer);
                resourceManager.CreateStructuredBufferSrv(_vertexBuffer, ref _vertexBufferWithViews.Srv);
                resourceManager.CreateStructuredBufferUav(_vertexBuffer, UnorderedAccessViewBufferFlags.None, ref _vertexBufferWithViews.Uav);
                
                _indexBufferWithViews.Buffer = _indexBuffer;
                const int stride = 3 * 4;
                resourceManager.SetupStructuredBuffer(_indexBufferData, stride * faceCount, stride, ref _indexBuffer);
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

        [Input(Guid = "608DE038-6C7A-43FC-BA89-374C7B1A318E")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "FDBAD44A-2504-453B-BFAE-976828372CC0")]
        public readonly InputSlot<float> Thickness = new InputSlot<float>();

        [Input(Guid = "99F5D952-8490-4930-B8AB-9D8E968183C6")]
        public readonly InputSlot<Size2> Segments = new InputSlot<Size2>();

        [Input(Guid = "770A164B-10E7-4145-B1C9-DAD1F564EC6B")]
        public readonly InputSlot<Vector2> Spin = new InputSlot<Vector2>();

        [Input(Guid = "F3E7341C-0C81-42AF-BA48-B43D345188C1")]
        public readonly InputSlot<Vector2> Portion = new InputSlot<Vector2>();

        // [Input(Guid = "1457EDC2-5F9B-4F72-9CB2-4CA40066F177")]
        // public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "2D083DC4-1576-4A44-8744-E0896424A6A9")]
        public readonly InputSlot<float> SmoothAngle = new InputSlot<float>();
    }
}