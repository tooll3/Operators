using System.Collections.Generic;
using System.Numerics;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_796a5efb_2ccf_4cae_b01c_d3f20a070181
{
    public class Points : Instance<Points>
    {
        [Output(Guid = "75443C4E-E91D-4AFC-89CC-C7F2AEF518A9")]
        public readonly Slot<Vector4> Result = new Slot<Vector4>();

        public Points()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            //Result.Value.Clear();
            // foreach (var input in Input.GetCollectedTypedInputs())
            // {
            //     Result.Value.Add(input.GetValue(context));
            // }
        }
        
        [Input(Guid = "CBCD7362-16C9-4B17-91D6-F6EFC7B235D7")]
        public readonly MultiInputSlot<float> Input = new MultiInputSlot<float>();
    }
}