using System.Diagnostics;
using T3.Core.Operator;

namespace T3.Operators.Types.Id_9cb4d49e_135b_400b_a035_2b02c5ea6a72
{
    public class BeatTime : Instance<BeatTime>
    {
        [Output(Guid = "b20573fe-7a7e-48e1-9370-744288ca6e32")]
        public readonly Slot<float> TimeInSeconds = new Slot<float>();

        public BeatTime()
        {
            TimeInSeconds.UpdateAction = Update;
            TimeInSeconds.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            TimeInSeconds.Value = (float)EvaluationContext.BeatTime;
        }
    }
}