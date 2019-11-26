using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class PartialScene03 : Instance<PartialScene03>
    {
        [Output(Guid = "64198e62-2e46-44a7-9b69-d89bcc0816ad")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

