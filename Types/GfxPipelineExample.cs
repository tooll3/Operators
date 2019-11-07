using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class GfxPipelineExample : Instance<GfxPipelineExample>
    {
        [Output(Guid = "2bb6126e-15b6-457d-a00b-c14d00fc5d41")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}
