using System;
using System.Diagnostics;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_11882635_4757_4cac_a024_70bb4e8b504c
{
    public class Counter : Instance<Counter>
    {
        [Output (Guid = "c53e3a03-3a6d-4547-abbf-7901b5045539")]
        public readonly Slot<float> Result = new Slot<float> ();
        
        public Counter()
        {
            Result.UpdateAction = Update;
            Result.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update (EvaluationContext context)
        {
            if (TriggerReset.GetValue(context))
            {
                _value = StartValue.GetValue(context);
            }
            
            
            var countTriggered = TriggerCount.GetValue(context);
            //Log.Debug("count.update  Triggered:" + countTriggered);
            if (countTriggered != _lastCountTriggered)
            {
                //Log.Debug("   changed!");
                if (countTriggered)
                    _value += Increment.GetValue(context);
                    
                _lastCountTriggered = countTriggered;
            }
            //_lastCountTriggered = countTriggered;

            Result.Value = _value;
        }

        private float _value;
        private bool _lastCountTriggered;
        

        [Input(Guid = "eefdb8ca-68e7-4e39-b302-22eb8930fb8c")]
        public readonly InputSlot<bool> TriggerCount = new InputSlot<bool>();

        [Input(Guid = "7BFBAE6B-FA0B-4E5A-8040-E0BE3600AFEB")]
        public readonly InputSlot<bool> TriggerReset = new InputSlot<bool>();

        [Input(Guid = "754CEBE3-AB6C-4877-9C32-C67FBAE9E4C2")]
        public readonly InputSlot<float> StartValue = new InputSlot<float>();

        [Input(Guid = "BCA3F7B2-A093-4CB3-89A5-0E2681760607")]
        public readonly InputSlot<float> Increment = new InputSlot<float>();

    }
}