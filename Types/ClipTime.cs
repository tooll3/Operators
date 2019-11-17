using System.Diagnostics;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ClipTime : Instance<ClipTime>
    {
        [Output(Guid = "3d9050a0-5688-4315-a6a4-fd8f1613eae2")]
        public readonly Slot<float> Time = new Slot<float>();

        public ClipTime()
        {
            Time.UpdateAction = Update;
            Time.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            
            Time.Value = (float)context.Time;
        }
    }
}