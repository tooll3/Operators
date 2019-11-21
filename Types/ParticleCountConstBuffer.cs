using System.Numerics;
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
            ResourceManager.Instance().SetupConstBuffer(Vector4.Zero, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(ParticleCountConstBuffer);
        }
    }
}