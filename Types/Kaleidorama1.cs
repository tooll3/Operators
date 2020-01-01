using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Kaleidorama1 : Instance<Kaleidorama1>
    {
        [Output(Guid = "83e0c40c-ddd9-4088-9683-41c2447938ef")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

