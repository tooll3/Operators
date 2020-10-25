using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_026869ee_b62f_481e_aadf_f8a1db77fe65 
{
    public class Compare : Instance<Compare>
    {
        [Output(Guid = "7149C7D2-242F-4D57-AC21-19E86700708A")]
        public readonly Slot<bool> Result = new Slot<bool>();

        public Compare()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var test = TestValue.GetValue(context);
            //var mod = Mod.GetValue(context);
            Result.Value =  Math.Abs(v-test)< 0.01f;
        }
        
        [Input(Guid = "8d98d88c-7a0e-4282-823e-4889ef286e5a")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "f1537faa-1bd2-44c9-b0ae-d06c5af5cdef")]
        public readonly InputSlot<float> TestValue = new InputSlot<float>();
    }
}
