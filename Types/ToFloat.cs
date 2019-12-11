using System.Collections.Generic;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ToFloat : Instance<ToFloat>
    {
        [Output(Guid = "DB1073A1-B9D8-4D52-BC5C-7AE8C0EE1AC3")]
        public readonly Slot<float> Result = new Slot<float>();
        
        public ToFloat()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = IntValue.GetValue(context);
        }
        
        [Input(Guid = "01809B63-4B4A-47BE-9588-98D5998DDB0C")]
        public readonly InputSlot<int> IntValue = new InputSlot<int>();
    }
}