using System.Diagnostics;
using T3.Core.Operator;

namespace T3.Operators.Types
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