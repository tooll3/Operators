using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;

namespace T3.Operators.Types.Id_6ab63114_6477_4ab2_a071_a66a64a6d2b9
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
