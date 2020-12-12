using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_cc07b314_4582_4c2c_84b8_bb32f59fc09b
{
    public class IntValue : Instance<IntValue>
    {
        [Output(Guid = "9f10edf1-b5f2-4873-ab48-8417fd176d35")]
        public readonly Slot<float> Result = new Slot<float>();

        public IntValue()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = Float.GetValue(context);
        }
        
        [Input(Guid = "92c9a175-ad44-49bd-9ab1-16ec89f69116")]
        public readonly InputSlot<float> Float = new InputSlot<float>();
    }
}
