using T3.Core;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_f9d453d1_04d9_43ef_9189_50008f93bcc2
{
    public class FluidFeedback : Instance<FluidFeedback>
    {
        [Output(Guid = "b9baba42-18b6-4792-929d-bf628ce8a488")]
        public readonly Slot<Texture2D> ColorBuffer = new Slot<Texture2D>();


        [Input(Guid = "ebd35d33-dc73-46ae-a82c-2060d750018a")]
        public readonly MultiInputSlot<Command> Command = new MultiInputSlot<Command>();

    }
}

