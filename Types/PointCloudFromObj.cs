using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_73f152ac_12d9_4ae9_856a_9a74637fd6f6
{
    public class PointCloudFromObj : Instance<PointCloudFromObj>
    {
        [Output(Guid = "90c70f78-7b3a-40a0-98a5-650be09871a4")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> PointCloudSrv = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        public PointCloudFromObj()
        {
            PointCloudSrv.UpdateAction = Update;
        }

        [StructLayout(LayoutKind.Explicit, Size = 48)]
        struct VertexEntry
        {
            [FieldOffset(0)]
            public SharpDX.Vector3 Pos;

            [FieldOffset(16)]
            public SharpDX.Vector3 Normal;

            [FieldOffset(32)]
            public SharpDX.Vector2 TexCoord;
        }

        struct Face
        {
            public Face(int v0, int v0n, int v0t, int v1, int v1n, int v1t, int v2, int v2n, int v2t)
            {
                V0 = v0;
                V0n = v0n;
                V0t = v0t;

                V1 = v1;
                V1n = v1n;
                V1t = v1t;

                V2 = v2;
                V2n = v2n;
                V2t = v2t;
            }

            public int V0;
            public int V0n;
            public int V0t;
            public int V1;
            public int V1n;
            public int V1t;
            public int V2;
            public int V2n;
            public int V2t;
        }

        public Buffer Buffer;

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            string path = Path.GetValue(context);
            if (string.IsNullOrEmpty(path) || !(new FileInfo(path).Exists))
                return;

            var vertices = new List<SharpDX.Vector3>();
            var texCoords = new List<SharpDX.Vector2>();
            var normals = new List<SharpDX.Vector3>();
            var faces = new List<Face>();

            using (var stream = new StreamReader(path))
            {
                try
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        var lineEntries = line.Split(' ');
                        switch (lineEntries[0])
                        {
                            case "v":
                            {
                                float x = float.Parse(lineEntries[1], CultureInfo.InvariantCulture);
                                float y = float.Parse(lineEntries[2], CultureInfo.InvariantCulture);
                                float z = float.Parse(lineEntries[3], CultureInfo.InvariantCulture);
                                vertices.Add(new Vector3(x, y, z));
                                break;
                            }
                            case "vt":
                            {
                                float u = float.Parse(lineEntries[1], CultureInfo.InvariantCulture);
                                float v = float.Parse(lineEntries[2], CultureInfo.InvariantCulture);
                                texCoords.Add(new Vector2(u, v));
                                break;
                            }
                            case "vn":
                            {
                                float x = float.Parse(lineEntries[1], CultureInfo.InvariantCulture);
                                float y = float.Parse(lineEntries[2], CultureInfo.InvariantCulture);
                                float z = float.Parse(lineEntries[3], CultureInfo.InvariantCulture);
                                normals.Add(new Vector3(x, y, z));
                                break;
                            }
                            case "f":
                            {
                                var v0 = lineEntries[1];
                                var v0entries = v0.Split('/');
                                int v0v = int.Parse(v0entries[0], CultureInfo.InvariantCulture) - 1;
                                int v0t = int.Parse(v0entries[1], CultureInfo.InvariantCulture) - 1;
                                int v0n = int.Parse(v0entries[2], CultureInfo.InvariantCulture) - 1;

                                var v1 = lineEntries[2];
                                var v1entries = v1.Split('/');
                                int v1v = int.Parse(v1entries[0], CultureInfo.InvariantCulture) - 1;
                                int v1t = int.Parse(v1entries[1], CultureInfo.InvariantCulture) - 1;
                                int v1n = int.Parse(v1entries[2], CultureInfo.InvariantCulture) - 1;

                                var v2 = lineEntries[3];
                                var v2entries = v2.Split('/');
                                int v2v = int.Parse(v2entries[0], CultureInfo.InvariantCulture) - 1;
                                int v2t = int.Parse(v2entries[1], CultureInfo.InvariantCulture) - 1;
                                int v2n = int.Parse(v2entries[2], CultureInfo.InvariantCulture) - 1;

                                faces.Add(new Face(v0v, v0n, v0t, v1v, v1n, v1t, v2v, v2n, v2t));
                                break;
                            }
                        }
                    }

                    int numVertexEntries = faces.Count * 3;
                    var bufferData = new VertexEntry[numVertexEntries];
                    for (int i = 0, faceIndex = 0; faceIndex < faces.Count; faceIndex++)
                    {
                        Face face = faces[faceIndex];
                        
                        bufferData[i].Pos = vertices[face.V0];
                        bufferData[i].Normal = normals[face.V0n];
                        bufferData[i].TexCoord = texCoords[face.V0t];
                        i++;
                        
                        bufferData[i].Pos = vertices[face.V1];
                        bufferData[i].Normal = normals[face.V1n];
                        bufferData[i].TexCoord = texCoords[face.V1t];
                        i++;

                        bufferData[i].Pos = vertices[face.V2];
                        bufferData[i].Normal = normals[face.V2n];
                        bufferData[i].TexCoord = texCoords[face.V2t];
                        i++;
                    }

                    int stride = 48;
                    resourceManager.SetupStructuredBuffer(bufferData, stride * numVertexEntries, stride, ref Buffer);
                    Buffer.DebugName = nameof(PointCloudFromObj);
                    resourceManager.CreateStructuredBufferSrv(Buffer, ref PointCloudSrv.Value);
                }
                catch (Exception e)
                {
                    Log.Error("Failed to load point cloud:" + e.Message);
                }
            }
        }

        [Input(Guid = "af396e7d-bda8-4c64-a109-b3f4c65f940d")]
        public readonly InputSlot<string> Path = new InputSlot<string>();
    }
}