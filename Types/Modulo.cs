using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Modulo : Instance<Modulo>
    {
        [Output(Guid = "4e4ebbcf-6b12-4ce7-9bec-78cd9049e239")]
        public readonly Slot<float> Result = new Slot<float>();

        public Modulo()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var mod = Mod.GetValue(context);
            Result.Value = v >= 0 
                               ? (v % mod)
                               : (Math.Abs(mod+v) % mod);
        }
        
        [Input(Guid = "8a401e5d-295d-4403-a3af-1d6b91ce3dba")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "62a8185f-32c0-41d2-b8be-d8c1d7178c00")]
        public readonly InputSlot<float> Mod = new InputSlot<float>();
    }
}
