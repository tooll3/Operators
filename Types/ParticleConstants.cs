using System.Collections.Generic;
using SharpDX;
using T3.Core.Logging;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ParticleConstants : Instance<ParticleConstants>
    {
        [Output(Guid = "9B7DCD58-CC08-479A-AF0B-C15B68768591")]
        public readonly Slot<int> Count = new Slot<int>();

        public ParticleConstants()
        {
            Count.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (MaxCount.DirtyFlag.IsDirty)
            {
                MaxCount.GetValue(context); // clear dirty flag
                int maxCount = 2048;
                Count.Value = maxCount;
                Log.Info("constants updated");
            }
        }

        [Input(Guid = "B0B1BCB1-9F02-4482-A323-89408B4AB347")]
        public readonly InputSlot<int> MaxCount = new InputSlot<int>(2048);
    }
}