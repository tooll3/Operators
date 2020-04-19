using T3.Core.Animation;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b724ea74_d5d7_4928_9cd1_7a7850e4e179
{
    public class SampleCurve : Instance<SampleCurve>
    {
        [Output(Guid = "fc51bee8-091c-4c66-a7df-12f6f69e3783")]
        public readonly Slot<float> Result = new Slot<float>();

//         [Output(Guid = "{DF114783-6C8D-47E2-99B0-8C97217657A5}")]
//         public readonly Slot<float> Result2 = new Slot<float>();

        public SampleCurve()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (Curve == null)
                return;
            
            var u = U.GetValue(context);
            var c = Curve.GetValue(context);
            if (c == null)
                return;
            
            var v = c.GetSampledValue(u);
            Result.Value = (float)c.GetSampledValue(u);
        }

        [Input(Guid = "108CB829-5F9E-4A45-BC6B-7CF40A0A0F89")]
        public readonly InputSlot<Curve> Curve = new InputSlot<T3.Core.Animation.Curve>();

        [Input(Guid = "2c24d4fe-6c96-4502-bf76-dac756a16215")]
        public readonly InputSlot<float> U = new InputSlot<float>();

        [Input(Guid = "5f144490-a6cd-4256-a5c0-959b84e01f43")]
        public readonly InputSlot<float> Input2 = new InputSlot<float>();

//         [Input(Guid = "{D7478BAA-41B4-4F83-873B-6267AA93BFA9}")]
//         public readonly InputSlot<float> Input3 = new InputSlot<float>();
// 
//         [Input(Guid = "{99A53560-8F62-4240-9ED4-800525CF2EF3}")]
//         public readonly InputSlot<float> Input4 = new InputSlot<float>();
    }
}
