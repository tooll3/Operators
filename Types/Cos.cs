using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Cos : Instance<Cos>
    {
        [Output(Guid = "4480F970-9E51-456A-8D66-D501FCA2C15B")]
        public readonly Slot<float> Result = new Slot<float>();

        public Cos()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = (float)Math.Cos(Input.GetValue(context));
        }
        
        [Input(Guid = "9764EFB1-57A8-48DA-B82E-4DCC2C3CB10A")]
        public readonly InputSlot<float> Input = new InputSlot<float>();
    }
}
