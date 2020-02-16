using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_48ab9824_76ca_4238_800f_9cf95311e6c0
{
    public class StringConcat : Instance<StringConcat>
    {
        [Output(Guid = "{E47BF25E-351A-44E6-84C6-AD3ABC93531A}")]
        public readonly Slot<string> Result = new Slot<string>();

        public StringConcat()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            // Result.Value = Input1.GetValue(context) + Input2.GetValue(context);
            Result.Value = string.Empty;
            foreach (var input in Input.GetCollectedTypedInputs())
            {
                Result.Value += input.GetValue(context);
            }
        }

        [Input(Guid = "{B5E72715-9339-484F-B197-5A28CD823798}")]
        public readonly MultiInputSlot<string> Input = new MultiInputSlot<string>();
    }
}