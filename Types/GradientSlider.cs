using T3.Core.DataTypes;
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
            var input = this.Input.GetValue(context);
            
            //TODO: Add gradient sampling
            Result.Value = input;
        }

        [Input(Guid = "EFF10FAD-CF95-4133-91DB-EFC41258CD1B")]
        public readonly InputSlot<Gradient> Gradient = new InputSlot<Gradient>();
        
        [Input(Guid = "a4527e01-f19a-4200-85e5-00144f3ce061")]
        public readonly InputSlot<float> Input = new InputSlot<float>();
    }
}
