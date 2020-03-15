using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_55b13dee_89f8_404f_b2fe_43d5e8c54536 
{
    public class Floor : Instance<Floor>
    {
        [Output(Guid = "5c54174b-c9e6-41de-b796-84ef4271dd20")]
        public readonly Slot<float> Result = new Slot<float>();

        public Floor()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var mod = Mod.GetValue(context);
            Result.Value = v - mod * (float)Math.Floor(v/mod);
        }
        
        [Input(Guid = "550289db-89cb-465c-a9d8-a16dbf23cc45")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "b68a33bc-6017-4f7d-97b3-e3d0099e1b7a")]
        public readonly InputSlot<float> Mod = new InputSlot<float>();
    }
}
