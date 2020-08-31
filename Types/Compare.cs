using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_026869ee_b62f_481e_aadf_f8a1db77fe65 
{
    public class Compare : Instance<Compare>
    {
        [Output(Guid = "c39b5769-1d1c-43a9-97d4-ea864e15c103")]
        public readonly Slot<float> Result = new Slot<float>();

        public Compare()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var mod = Mod.GetValue(context);
            Result.Value = v - mod * (float)Math.Floor(v/mod);
        }
        
        [Input(Guid = "8d98d88c-7a0e-4282-823e-4889ef286e5a")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "f1537faa-1bd2-44c9-b0ae-d06c5af5cdef")]
        public readonly InputSlot<float> Mod = new InputSlot<float>();
    }
}
