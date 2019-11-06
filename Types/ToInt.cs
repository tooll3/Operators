using System.Collections.Generic;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ToInt : Instance<ToInt>
    {
        [Output(Guid = "1EB7C5C4-0982-43F4-B14D-524571E3CDDA")]
        public readonly Slot<int> Integer = new Slot<int>();

        public ToInt()
        {
            Integer.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Integer.Value = (int)FloatValue.GetValue(context);
        }

        [Input(Guid = "AF866A6C-1AB0-43C0-9E8A-5D25C300E128")]
        public readonly InputSlot<float> FloatValue = new InputSlot<float>();
    }
}