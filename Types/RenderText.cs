using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_fd31d208_12fe_46bf_bfa3_101211f8f497
{
    public class RenderText : Instance<RenderText>
    {
        [Output(Guid = "3f8b20a7-c8b8-45ab-86a1-0efcd927358e")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "f1f1be0e-d5bc-4940-bbc1-88bfa958f0e1")]
        public readonly InputSlot<string> Text = new InputSlot<string>();

        [Input(Guid = "0e5f05b4-5e8a-4f6d-8cac-03b04649eb67")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "15b1e70a-561c-4ba3-a078-eead7383e41e")]
        public readonly InputSlot<System.Numerics.Vector3> Params3 = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "50c9ab21-39f4-4e92-b5a7-ad9e60ae6ec3")]
        public readonly InputSlot<string> FontPath = new InputSlot<string>();
    }
}

