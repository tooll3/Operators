using System;
using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_59b810f1_7849_40a7_ae10_7e8008685311
{
    public class PointsToBuffer : Instance<PointsToBuffer>
    {
        [Output(Guid = "00027a91-db2f-4eed-a340-3cdf692be853")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> PointCloudSrv = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Output(Guid = "d92f74db-35c1-4ca0-8cbf-8c813147e03e")]
        public readonly Slot<int> EmitterId = new Slot<int>();

        public PointsToBuffer()
        {
            _id = EmitterCounter.GetId();
            EmitterId.Value = _id;
            PointCloudSrv.UpdateAction = Update;
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        struct BufferEntry
        {
            [FieldOffset(0)]
            public SharpDX.Vector3 Pos;

            [FieldOffset(12)]
            public int Id;

            [FieldOffset(16)]
            public SharpDX.Vector4 Color;
        }

        private Buffer _buffer;
        private int _id;

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();

            var numEntries = Count.GetValue(context);
            numEntries = Math.Max(numEntries, 1);
            numEntries = Math.Min(numEntries, 500000);

            var bufferData = new BufferEntry[numEntries];

            var color = Color.GetValue(context);
            var random = new Random(Seed.GetValue(context));
            var radius = Radius.GetValue(context);
            var objectToWorld = context.ObjectToWorld;

            for (int index = 0; index < numEntries; index++)
            {
                float u = random.NextFloat(0, 1);
                float v = random.NextFloat(0, 1);

                float theta = SharpDX.MathUtil.TwoPi * u;
                float z = 1.0f - 2.0f * v;
                float phi = (float)Math.Acos(z);
                float sinPhi = (float)Math.Sin(phi);
                float x = sinPhi * (float)Math.Cos(theta);
                float y = sinPhi * (float)Math.Sin(theta);

                var posInObject = new Vector3(x, y, z);
                // dir *= radius;
                posInObject *= random.NextFloat(0.0f, radius);

                var posInWorld = Vector3.Transform(posInObject, objectToWorld);

                bufferData[index].Pos = new Vector3(posInWorld.X, posInWorld.Y, posInWorld.Z);
                bufferData[index].Id = _id;
                bufferData[index].Color = new Vector4(color.X, color.Y, color.Z, color.W);
            }

            var stride = 32;
            resourceManager.SetupStructuredBuffer(bufferData, stride * numEntries, stride, ref _buffer);
            resourceManager.CreateStructuredBufferSrv(_buffer, ref PointCloudSrv.Value);
        }

        [Input(Guid = "d266cbb2-130c-4bc7-87f7-070c34052114")]
        public readonly InputSlot<int> Count = new InputSlot<int>();

        [Input(Guid = "17c2666b-77b0-481f-a346-23f5fb0a6668")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "7eaea9f7-c59a-4281-ab96-d214aee9ae3a")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "c8e59398-d5ec-420b-844e-58bf8c13b0ef")]
        public readonly InputSlot<int> Seed = new InputSlot<int>();
    }
}