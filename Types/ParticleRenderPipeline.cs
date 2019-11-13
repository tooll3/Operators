using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ParticleRenderPipeline : Instance<ParticleRenderPipeline>
    {
        [Output(Guid = "229cf691-ee88-4e84-ad4f-90c51ecb14df")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

