using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_d3fb5baf_43f8_4983_a1d9_42f4005a3af0
{
    public class PeakLevel : Instance<PeakLevel>
    {
        [Output(Guid = "6fe37109-0177-4823-9466-eaa49adb19d4")]
        public readonly Slot<float> Result = new Slot<float>();

        public PeakLevel()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var value = Value.GetValue(context);
            if (value > _lastPeak)
            {
                _lastPeak = value;
            }
            else
            {
                _lastPeak *= 0.96f;
            }

            Result.Value = _lastPeak;
        }

        private float _lastPeak;

        [Input(Guid = "88ed25d3-ab67-47da-ad38-2f0126ce0492")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "30f474d1-5c5d-4e39-a31a-b4554809eae0")]
        public readonly InputSlot<float> RangeInMin = new InputSlot<float>();

        [Input(Guid = "783c45d7-cc9c-42ab-93cb-17333cf6243c")]
        public readonly InputSlot<float> RangeInMax = new InputSlot<float>();

        [Input(Guid = "6c67d791-a28b-4acd-ad29-8b226dabaa30")]
        public readonly InputSlot<float> RangeOutMin = new InputSlot<float>();

        [Input(Guid = "4876a122-5acd-4b08-ab34-7e943e903381")]
        public readonly InputSlot<float> RangeOutMax = new InputSlot<float>();

        [Input(Guid = "24070e02-04cd-49d9-8f34-ae420e296e50")]
        public readonly InputSlot<bool> Clamp = new InputSlot<bool>();
    }
}