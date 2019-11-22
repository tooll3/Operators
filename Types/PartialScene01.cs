using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class PartialScene01 : Instance<PartialScene01>
    {
        [Output(Guid = "92f6b42b-163f-45c1-9690-7f6cd3274601")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

