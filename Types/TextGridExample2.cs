using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_3e404381_9246_4eb3_ba42_58b193e68ed8
{
    public class TextGridExample2 : Instance<TextGridExample2>
    {
        [Output(Guid = "abe3ba0c-0614-48dd-9e74-4386e72ee448")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Output(Guid = "aeacc3ad-2ad0-4c0b-91a2-5e31e9d70966")]
        public readonly TimeClipSlot<SharpDX.Direct3D11.Texture2D> Output2 = new TimeClipSlot<SharpDX.Direct3D11.Texture2D>();


    }
}

