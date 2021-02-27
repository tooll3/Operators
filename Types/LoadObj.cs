using System.IO;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Interfaces;
using T3.Core.Operator.Slots;
using T3.Core.Rendering;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_be52b670_9749_4c0d_89f0_d8b101395227
{
    public class LoadObj : Instance<LoadObj>, IDescriptiveGraphNode
    {
        [Output(Guid = "1F4E7CAC-1F62-4633-B0F3-A3017A026753")]
        public readonly Slot<MeshBuffers> Data = new Slot<MeshBuffers>();
        
        public LoadObj()
        {
            Data.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var path = Path.GetValue(context);
            if (path != _lastFilePath)
            {
                var mesh = ObjMesh.LoadFromFile(path);
                _lastFilePath = path;
                
                if (mesh == null || mesh.DistinctDistinctVertices.Count == 0)
                {
                    Log.Warning($"Can't read file {path}");
                    return;
                }

                var resourceManager = ResourceManager.Instance();

                // Create Vertex buffer
                {
                    var verticesCount = mesh.DistinctDistinctVertices.Count;
                    if (_vertexBufferData.Length != verticesCount)
                        _vertexBufferData = new PbrVertex[verticesCount];

                    for (var vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                    {

                        var vertex = mesh.DistinctDistinctVertices[vertexIndex];
                        _vertexBufferData[vertexIndex] = new PbrVertex
                                                             {
                                                                 Position = mesh.Positions[vertex.PositionIndex],
                                                                 Normal = mesh.Normals[vertex.NormalIndex],
                                                                 Tangent = mesh.VertexTangents[vertexIndex],
                                                                 Bitangent = mesh.VertexBinormals[vertexIndex],
                                                                 Texcoord = mesh.TexCoords[vertex.TextureCoordsIndex]
                                                             };
                    }

                    _vertexBufferWithViews.Buffer = _vertexBuffer;
                    resourceManager.SetupStructuredBuffer(_vertexBufferData, PbrVertex.Stride * verticesCount, PbrVertex.Stride, ref _vertexBuffer);
                    resourceManager.CreateStructuredBufferSrv(_vertexBuffer, ref _vertexBufferWithViews.Srv);
                    resourceManager.CreateStructuredBufferUav(_vertexBuffer, UnorderedAccessViewBufferFlags.None, ref _vertexBufferWithViews.Uav);
                }

                // Create Index buffer
                {
                    var faceCount = mesh.Faces.Count;
                    if (_indexBufferData.Length != faceCount)
                        _indexBufferData = new SharpDX.Int3[faceCount];

                    for (var faceIndex = 0; faceIndex < faceCount; faceIndex++)
                    {
                        var face = mesh.Faces[faceIndex];
                        _indexBufferData[faceIndex] = new SharpDX.Int3(
                                                                       mesh.GetVertexIndex(face.V0, face.V0n, face.V0t),
                                                                       mesh.GetVertexIndex(face.V1, face.V1n, face.V1t),
                                                                       mesh.GetVertexIndex(face.V2, face.V2n, face.V2t)
                                                                      );
                    }

                    _indexBufferWithViews.Buffer = _indexBuffer;
                    const int stride = 3 * 4;
                    resourceManager.SetupStructuredBuffer(_indexBufferData, stride * faceCount, stride, ref _indexBuffer);
                    resourceManager.CreateStructuredBufferSrv(_indexBuffer, ref _indexBufferWithViews.Srv);
                    resourceManager.CreateStructuredBufferUav(_indexBuffer, UnorderedAccessViewBufferFlags.None, ref _indexBufferWithViews.Uav);
                }
                //Log.Debug($"Updated mesh buffers: {_vertexBuffer} for {path}");

                _description = System.IO.Path.GetFileName(_lastFilePath);
            }
            _dataBuffers.VertexBuffer = _vertexBufferWithViews;
            _dataBuffers.IndicesBuffer = _indexBufferWithViews;
            Data.Value = _dataBuffers;
        }

        
        public string GetDescriptiveString()
        {
            return _description;
        }
        
        
        private string _description;

        private string _lastFilePath;
        private readonly MeshBuffers _dataBuffers = new MeshBuffers();
        private Buffer _vertexBuffer;
        private PbrVertex[] _vertexBufferData = new PbrVertex[0];
        private readonly BufferWithViews _vertexBufferWithViews = new BufferWithViews();

        private Buffer _indexBuffer;
        private SharpDX.Int3[] _indexBufferData = new SharpDX.Int3[0];
        private readonly BufferWithViews _indexBufferWithViews = new BufferWithViews();

        [Input(Guid = "7d576017-89bd-4813-bc9b-70214efe6a27")]
        public readonly InputSlot<string> Path = new InputSlot<string>();

    }
}