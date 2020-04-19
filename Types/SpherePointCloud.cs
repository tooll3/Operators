using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_491d5fc3_75f4_4ddd_854b_cd1769166fa6
{
    public class SpherePointCloud : Instance<SpherePointCloud>
    {
        [Output(Guid = "059737a1-09ce-42d0-a16a-e5d95cd0c8e1")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> PointCloudSrv = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        public SpherePointCloud()
        {
            PointCloudSrv.UpdateAction = Update;
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        struct BufferEntry
        {
            [FieldOffset(0)]
            public SharpDX.Vector4 Pos;

            [FieldOffset(16)]
            public SharpDX.Vector4 Color;
        }

        public Buffer Buffer;

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            // string path = Path.GetValue(context);
            // if (string.IsNullOrEmpty(path) || !(new FileInfo(path).Exists))
            //     return;

            var numEntries = Count.GetValue(context);
            numEntries = Math.Max(numEntries, 1);
            numEntries = Math.Min(numEntries, 10000);
            
            var bufferData = new BufferEntry[numEntries];

            var color = Color.GetValue(context);
            var random = new Random(Seed.GetValue(context));
            var radius = Radius.GetValue(context);
            for (int index = 0; index < numEntries; index++)
            {
                var v = new Vector3(
                                    random.NextFloat(0, radius),
                                    random.NextFloat(0, radius),
                                    random.NextFloat(0, radius)
                                    );
                v.Normalize();
                bufferData[index].Pos = new Vector4(v.X, v.Y, v.Z, 1.0f);
                bufferData[index].Color = new Vector4(color.X, color.Y, color.Z, color.W);
            }
            
            var stride = 32;
            resourceManager.SetupStructuredBuffer(bufferData, stride * numEntries, stride, ref Buffer);
            resourceManager.CreateStructuredBufferSrv(Buffer, ref PointCloudSrv.Value);
        }
        
        [Input(Guid = "0295ce1a-cdc5-44ec-bc61-fb73d2983fa3")]
        public readonly InputSlot<string> Path = new InputSlot<string>();

        [Input(Guid = "AD7F8575-A978-4A09-89F8-CF6F6EE8808C")]
        public readonly InputSlot<int> Count = new InputSlot<int>();

        [Input(Guid = "57543B8C-4AD6-4B0F-B270-7A81AD8908D9")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        
        [Input(Guid = "E5F8D8C8-4E22-4489-859D-7DED280ECE9F")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();
        
        [Input(Guid = "99924178-BC02-470B-9750-2E0AC9B702B5")]
        public readonly InputSlot<int> Seed = new InputSlot<int>();

    }
}