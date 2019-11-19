using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Sin : Instance<Sin>
    {
        [Output(Guid = "55D5C012-0026-4390-9B40-38BD1BBFDAD4")]
        public readonly Slot<float> Result = new Slot<float>();

        public Sin()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = (float)Math.Sin(Input.GetValue(context));
        }
        
        [Input(Guid = "9C66D915-AF91-4ECD-955A-D9C15EF3EDDA")]
        public readonly InputSlot<float> Input = new InputSlot<float>();
    }
}
