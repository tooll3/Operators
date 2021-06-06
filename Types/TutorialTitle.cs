using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b0248e6e_a82b_48d1_ac65_ee7b36038478
{
    public class TutorialTitle : Instance<TutorialTitle>
    {
        [Output(Guid = "ad10e89c-3350-495d-a992-5f7d371defc8")]
        public readonly Slot<Texture2D> ColorBuffer = new Slot<Texture2D>();

        [Input(Guid = "9b430a01-9dfe-4fe5-a946-29496e58f82f")]
        public readonly InputSlot<string> Input = new InputSlot<string>();


    }
}

