using System.Diagnostics;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c513c58d_e45c_408d_a0b8_250c9af31545
{
    public class HasValueIncreased : Instance<HasValueIncreased>
    {
        [Output(Guid = "598E38D5-2347-4B93-A7A4-A23190D95DCD")]
        public readonly Slot<bool> HasIncreased = new Slot<bool>();
        

        public HasValueIncreased()
        {
            HasIncreased.UpdateAction = Update;
            //HasIncreased.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var result = v > _lastValue;
            if (result == _lastResult)
                return;

            HasIncreased.Value = result;
            _lastValue = v;
            _lastResult = result;
        }

        private bool _lastResult = false;
        private float _lastValue = 0;
        
        [Input(Guid = "ed88c6c7-1ea2-4593-9589-ec670afb4654")]
        public readonly InputSlot<float> Value = new InputSlot<float>();
    }
}