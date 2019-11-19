﻿using System.Diagnostics;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class RunTime : Instance<RunTime>
    {
        [Output(Guid = "{1C34D39C-0BEF-4C4A-A3E4-DCB8D5664F3B}")]
        public readonly Slot<float> TimeInSeconds = new Slot<float>();

        public RunTime()
        {
            TimeInSeconds.UpdateAction = Update;
            TimeInSeconds.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            TimeInSeconds.Value = (float)EvaluationContext.RunTime;
        }
    }
}