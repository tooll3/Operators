using ManagedBass;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_13835d87_7bce_4e2c_9ca7_4caa02aeca89
{
    public class BassWasapi : Instance<BassWasapi>
    {
        [Output(Guid = "b0dfcd45-f99a-4e4b-af23-d7e0fb3b5434")]
        public readonly Slot<float> Result = new Slot<float>();

        public BassWasapi()
        {
            Result.UpdateAction = Update;
            int device = Bass.DefaultDevice;
            ManagedBass.Wasapi.BassWasapi.Init(device);
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = Input1.GetValue(context) + Input2.GetValue(context);
        }


        [Input(Guid = "ba1bf196-2197-40df-97d5-9984005d90d5")]
        public readonly InputSlot<float> Input1 = new InputSlot<float>();

        [Input(Guid = "87e6d385-5976-422f-8719-c6b32dcb7fa8")]
        public readonly InputSlot<float> Input2 = new InputSlot<float>();

    }
}
