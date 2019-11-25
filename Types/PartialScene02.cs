using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class PartialScene02 : Instance<PartialScene02>
    {
        [Output(Guid = "0b90f691-ba5c-497d-afad-4daffb60cf55")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

