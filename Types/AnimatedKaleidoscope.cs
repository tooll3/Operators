using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class AnimatedKaleidoscope : Instance<AnimatedKaleidoscope>
    {
        [Output(Guid = "b68d2405-821b-4347-bd59-2f62fa6bcf6f")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

