using System.Diagnostics;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_9cb4d49e_135b_400b_a035_2b02c5ea6a72
{
    public class BeatTime : Instance<BeatTime>
    {
        [Output(Guid = "b20573fe-7a7e-48e1-9370-744288ca6e32")]
        public readonly Slot<float> TimeInBeats = new Slot<float>();

        public BeatTime()
        {
            TimeInBeats.UpdateAction = Update;
            TimeInBeats.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            TimeInBeats.Value = (float)EvaluationContext.BeatTime;
        }
    }
}