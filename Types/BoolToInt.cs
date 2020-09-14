using System.Collections.Generic;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_cd43942a_887e_4e34_bc54_0c2e5e8bc2af
{
    public class BoolToInt : Instance<BoolToInt>
    {
        [Output(Guid = "b0cfa6f9-3c3d-4499-b21a-5904d1cb3bd7")]
        public readonly Slot<int> Result = new Slot<int>();
        
        public BoolToInt()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = BoolValue.GetValue(context) ? 1 : 0;
        }
        
        [Input(Guid = "c644165f-3901-4dbf-8091-05f958e668e5")]
        public readonly InputSlot<bool> BoolValue = new InputSlot<bool>();
    }
}