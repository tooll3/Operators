using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using T3.Core.Rendering;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_be52b670_9749_4c0d_89f0_d8b101395227
{
    public class LoadObjAsMesh : Instance<LoadObjAsMesh>
    {
        [Output(Guid = "C84342BA-0271-4C56-A642-B02BD401D246")]
        public readonly Slot<BufferWithViews> VertexBuffer = new Slot<BufferWithViews>();

        [Output(Guid = "FB41F6C6-A5EB-483F-AE7D-85A31095B42F")]
        public readonly Slot<BufferWithViews> IndexBuffer = new Slot<BufferWithViews>();

        public LoadObjAsMesh()
        {
            VertexBuffer.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var path = Path.GetValue(context);
            var mesh = ObjMesh.LoadFromFile(path);

            if (mesh == null || mesh.DistinctVertices.Count == 0)
            {
                Log.Warning($"Can't read file {path}");
                return;
            }

            var resourceManager = ResourceManager.Instance();

            // Create Vertex buffer
            {
                var verticesCount = mesh.DistinctVertices.Count;
                if (_vertexBufferData.Length != verticesCount)
                    _vertexBufferData = new PbrVertex[verticesCount];

                for (var index = 0; index < verticesCount; index++)
                {
                    var vertex = mesh.DistinctVertices[index];
                    _vertexBufferData[index] = new PbrVertex
                                                   {
                                                       Position = mesh.Vertices[vertex.PositionIndex],
                                                       Normal = mesh.Normals[vertex.NormalIndex],
                                                       Tangent = default,
                                                       Bitangent = default,
                                                       Texcoord = mesh.TexCoords[vertex.TextureCoordsIndex]
                                                   };
                }

                _vertexBufferWithViews.Buffer = _vertexBuffer;
                resourceManager.SetupStructuredBuffer(_vertexBufferData, PbrVertex.Stride * verticesCount, PbrVertex.Stride, ref _vertexBuffer);
                resourceManager.CreateStructuredBufferSrv(_vertexBuffer, ref _vertexBufferWithViews.Srv);
                resourceManager.CreateStructuredBufferUav(_vertexBuffer, UnorderedAccessViewBufferFlags.None, ref _vertexBufferWithViews.Uav);
                VertexBuffer.Value = _vertexBufferWithViews;
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
                IndexBuffer.Value = _indexBufferWithViews;
            }            
        }

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