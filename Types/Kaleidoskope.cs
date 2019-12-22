using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Kaleidoskope : Instance<Kaleidoskope>
    {
        [Output(Guid = "3bae278b-2555-43a8-8837-e164c87a0900")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

