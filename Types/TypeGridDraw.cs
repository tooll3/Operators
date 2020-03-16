using T3.Core.Operator.Helper;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ad28819d_be62_4ed7_932a_fc861562983d
{
    public class TypeGridDraw : Instance<TypeGridDraw>
    {
        [Output(Guid = "982bd425-e781-42ad-9c58-f026fb6f193c")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "7f86c4ea-48d1-4d3a-9a5a-cfbca0da4daa")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "70cf8cfb-56a3-47bf-9dbc-fd1b5a2f7e82")]
        public readonly InputSlot<float> Size = new InputSlot<float>();
    }
}

