using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_f0acd1a4_7a98_43ab_a807_6d1bd3e92169
{
    public class Remap : Instance<Remap>
    {
        [Output(Guid = "de6e6f65-cb51-49f1-bb90-34ed1ec963c1")]
        public readonly Slot<float> Result = new Slot<float>();

        public Remap()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var value = Value.GetValue(context);
            var inMin = RangeInMin.GetValue(context);
            var inMax = RangeInMax.GetValue(context);
            var outMin = RangeOutMin.GetValue(context);
            var outMax = RangeOutMax.GetValue(context);
            var clamp = Clamp.GetValue(context);

            var factor = (value - inMin) / (inMax - inMin);
            var v = factor * (outMax - outMin) + outMin;
            if (clamp)
            {
                if (v > outMax)
                {
                    v = outMax;
                }
                else if (v < outMin)
                {
                    v = outMin;
                }
            }

            Result.Value = v;
        }

        [Input(Guid = "40606d4e-acaf-4f23-a845-16f0eb9b73cf")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "edb98f34-d019-47f6-b275-e5a80061e1f7")]
        public readonly InputSlot<float> RangeInMin = new InputSlot<float>();

        [Input(Guid = "CD369755-5062-4934-8F37-E3A5CC9963DF")]
        public readonly InputSlot<float> RangeInMax = new InputSlot<float>();

        [Input(Guid = "F2BAF278-ADDE-42DE-AFCE-336B6C8D0387")]
        public readonly InputSlot<float> RangeOutMin = new InputSlot<float>();

        [Input(Guid = "252276FB-8DE1-42CC-BA41-07D6862015BD")]
        public readonly InputSlot<float> RangeOutMax = new InputSlot<float>();

        [Input(Guid = "413C8072-7E0A-46C9-A48E-87272AB72CCB")]
        public readonly InputSlot<bool> Clamp = new InputSlot<bool>();
    }
}