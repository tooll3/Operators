using System.Runtime.InteropServices;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class ParticleCountConstBuffer : Instance<ParticleCountConstBuffer>
    {
        [Output(Guid = "7CE24D2E-C4B8-4F56-8086-8AF4FAC1ABB2")]
        public readonly Slot<Buffer> Buffer = new Slot<Buffer>();

        public ParticleCountConstBuffer()
        {
            Buffer.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var bufferContent = new BufferLayout();
            ResourceManager.Instance().SetupConstBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(ParticleCountConstBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct BufferLayout
        {
            public BufferLayout(int i = 0)
            {
                Param1 = 0;
                Param2 = 0;
                Param3 = 0;
                Param4 = 100;
            }

            [FieldOffset(0)]
            public int Param1;
            [FieldOffset(4)]
            public int Param2;
            [FieldOffset(8)]
            public int Param3;
            [FieldOffset(12)]
            public int Param4;
        }       
    }
}