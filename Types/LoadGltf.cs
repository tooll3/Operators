using System;
using System.Collections.Generic;
using System.Linq;
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
using SharpGLTF.Schema2;

namespace T3.Operators.Types.Id_1271a1ec_ddbf_4425_a999_2d4354bcd15b
{
    public class LoadGltf : Instance<LoadGltf>
    {
        [Output(Guid = "3dc3092d-58ee-457c-bb39-0870e6934dc5")]
        public readonly Slot<MeshBuffers> Data = new Slot<MeshBuffers>();

        public LoadGltf()
        {
            //Data.UpdateAction = Update;
        }
        
        private void Update(EvaluationContext context)
        {
            var path = Path.GetValue(context);
            if (path != _lastFilePath || SortVertices.DirtyFlag.IsDirty)
            {
                try
                {
                    var model = SharpGLTF.Schema2.ModelRoot.Load(path);
                    foreach (var node in model.LogicalNodes)
                    {
                        if (node.Mesh == null)
                            continue;
        
                        var mesh2 = node.Mesh;
                        //Log.Debug($"{node.Name.ToString()}  skin: {node.IsSkinSkeleton}  mesh:{node.Mesh}");
                        foreach (var p in mesh2.Primitives)
                        {
                            var positions = p.GetVertices("POSITION").AsVector3Array();
                            var normals = p.GetVertices("NORMAL").AsVector3Array();
                            var uvs = p.GetVertices("TEXCOORD_0").AsVector2Array();
                            
                            if (positions.Count == 0)
                            {
                                Log.Warning($"{node.Name} has incomplete vertex information");
                                continue;
                            }
        
                            var indices =  p.GetTriangleIndices().ToList();
        
                            Log.Debug($" pos: {positions.Count} normal: {normals.Count} uvs {uvs.Count}  faces: {indices.Count}");
        
                            if (positions.Count == 0 || indices.Count == 0)
                            {
                                Log.Warning($"Mesh node is empty {path}");
                                continue;
                            }
        
                            _lastFilePath = path;
        
                            var resourceManager = ResourceManager.Instance();
        
                            var newData = new DataSet();
        
                            // Create Vertex buffer
                            {
                                var verticesCount = positions.Count;// mesh.DistinctDistinctVertices.Count;
                                if (newData.VertexBufferData.Length != verticesCount)
                                    newData.VertexBufferData = new PbrVertex[verticesCount];
        
                                for (var vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                                {
                                    newData.VertexBufferData[vertexIndex] = new PbrVertex
                                                                                {
                                                                                    Position = new SharpDX.Vector3(
                                                                                     positions[vertexIndex].X,
                                                                                    positions[vertexIndex].Y,
                                                                                    positions[vertexIndex].Z)
                                                                                    ,
                                                                                    Normal = new SharpDX.Vector3(normals[vertexIndex].X,
                                                                                        normals[vertexIndex].Y,
                                                                                        normals[vertexIndex].Z),
                                                                                    Tangent = Vector3.Right,
                                                                                    Bitangent = Vector3.Up,
                                                                                    Texcoord = new SharpDX.Vector2(uvs[vertexIndex].X, uvs[vertexIndex].Y),
                                                                                    Selection = 1,
                                                                                };
                                }
        
                                newData.VertexBufferWithViews.Buffer = newData.VertexBuffer;
                                resourceManager.SetupStructuredBuffer(newData.VertexBufferData, PbrVertex.Stride * verticesCount,
                                                                      PbrVertex.Stride,
                                                                      ref newData.VertexBuffer);
                                resourceManager.CreateStructuredBufferSrv(newData.VertexBuffer, ref newData.VertexBufferWithViews.Srv);
                                resourceManager.CreateStructuredBufferUav(newData.VertexBuffer, UnorderedAccessViewBufferFlags.None,
                                                                          ref newData.VertexBufferWithViews.Uav);
                            }
        
                            // Create Index buffer
                            {
                                var faceCount = indices.Count;
                                if (newData.IndexBufferData.Length != faceCount)
                                    newData.IndexBufferData = new SharpDX.Int3[faceCount];
        
                                for (var faceIndex = 0; faceIndex < faceCount; faceIndex++)
                                {
                                    var (a, b, c) = indices[faceIndex];

                                    newData.IndexBufferData[faceIndex]
                                        = new SharpDX.Int3(a,b,c);
                                }
        
                                newData.IndexBufferWithViews.Buffer = newData.IndexBuffer;
                                const int stride = 3 * 4;
                                resourceManager.SetupStructuredBuffer(newData.IndexBufferData, stride * faceCount, stride,
                                                                      ref newData.IndexBuffer);
                                resourceManager.CreateStructuredBufferSrv(newData.IndexBuffer, ref newData.IndexBufferWithViews.Srv);
                                resourceManager.CreateStructuredBufferUav(newData.IndexBuffer, UnorderedAccessViewBufferFlags.None,
                                                                          ref newData.IndexBufferWithViews.Uav);
                            }
                            _data = newData;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Warning($"Failed to load {path}: {e.Message}");
                }
            }
        
            _data.DataBuffers.VertexBuffer = _data.VertexBufferWithViews;
            _data.DataBuffers.IndicesBuffer = _data.IndexBufferWithViews;
            Data.Value = _data.DataBuffers;
        }

        public string GetDescriptiveString()
        {
            return _description;
        }

        private string _description;
        private string _lastFilePath;
        private DataSet _data = new DataSet();

        private class DataSet
        {
            public readonly MeshBuffers DataBuffers = new MeshBuffers();

            public Buffer VertexBuffer;
            public PbrVertex[] VertexBufferData = new PbrVertex[0];
            public readonly BufferWithViews VertexBufferWithViews = new BufferWithViews();

            public Buffer IndexBuffer;
            public SharpDX.Int3[] IndexBufferData = new SharpDX.Int3[0];
            public readonly BufferWithViews IndexBufferWithViews = new BufferWithViews();
        }

        private static readonly Dictionary<string, DataSet> MeshBufferCache = new Dictionary<string, DataSet>();

        [Input(Guid = "901443b6-45f2-4bce-988a-9fc7503d351f")]
        public readonly InputSlot<string> Path = new InputSlot<string>();

        [Input(Guid = "2f355e92-c444-4717-9601-f6d2f7680b6c")]
        public readonly InputSlot<bool> UseGPUCaching = new InputSlot<bool>();

        [Input(Guid = "da0fda36-9431-4ebf-9a5b-b1b72dff9f54")]
        public readonly InputSlot<int> SortVertices = new InputSlot<int>();
    }
}