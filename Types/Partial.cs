using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_a9080ad4_6cb2_4d5b_ad07_1cce239ee787
{
    public class Partial : Instance<Partial>
    {
        [Output(Guid = "1668e648-7173-4d8b-9e0c-979772f5a396")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();

        [Output(Guid = "9fccfe48-f1ca-42b0-9aef-e6d6e8f39155")]
        public readonly Slot<Texture2D> TextureOutput2 = new Slot<Texture2D>();


    }
}

