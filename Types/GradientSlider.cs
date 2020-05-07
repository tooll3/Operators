using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_8211249d_7a26_4ad0_8d84_56da72a5c536
{
    public class GradientSlider : Instance<GradientSlider>
    {
        [Output(Guid = "8c950a47-9642-4ad5-8bed-a7ea5acd27b6")]
        public readonly Slot<float> Result = new Slot<float>();

        public GradientSlider()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var min = Min.GetValue(context);
            var max = Max.GetValue(context);
            var input = this.Input.GetValue(context);
            Result.Value = (max - min) * input + min;
        }

        [Input(Guid = "a4527e01-f19a-4200-85e5-00144f3ce061")]
        public readonly InputSlot<float> Input = new InputSlot<float>();
        
        [Input(Guid = "039be267-5b3f-45a0-ba8d-0fc591405eb0")]
        public readonly InputSlot<float> Min = new InputSlot<float>();

        [Input(Guid = "cd2744d4-c9cf-46c5-bdca-a60fd4f83ff6")]
        public readonly InputSlot<float> Max = new InputSlot<float>();


    }
}
