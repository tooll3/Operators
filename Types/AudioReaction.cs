using System;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_f8aed421_5e0e_4d1f_993c_1801153ebba8
{
    public class AudioReaction : Instance<AudioReaction>
    {
        [Output(Guid = "2aa4d0cb-c49d-41ce-aa74-794cc8682590")]
        public readonly Slot<float> Result = new Slot<float>();

        public AudioReaction()
        {
            Result.UpdateAction = Update;
            //Variable.DirtyFlag.Trigger |= DirtyFlagTrigger.Animated;
        }

        private void Update(EvaluationContext context)
        {
            // string variableName = Variable.GetValue(context);
            // if (context.FloatVariables.TryGetValue(variableName, out float value))
            // {
            //     // Log.Debug($"{variableName} : {value}");
            //     Result.Value = value;
            // }
        }

        private enum FrequencyBands
        {
            Bass,
            Highs,
        }
        
        private enum Modes
        {
            TimeSincePeak,
            PeakCount,
            LinearPeaks,
            DecayingPeaks,
            MovingSum,
        }
        
        [Input(Guid = "15F841F5-5153-4383-90B9-F6A4F72D5D6B", MappedType = typeof(FrequencyBands))]
        public readonly InputSlot<int> Band = new InputSlot<int>();
        
        [Input(Guid = "D9069774-188B-4A5E-976A-053D0C893503", MappedType = typeof(Modes))]
        public readonly InputSlot<int> Mode = new InputSlot<int>();
        
        [Input(Guid = "EC0FE09B-B925-4B14-8186-8C32B65AF9BB")]
        public readonly InputSlot<float> Amplitude = new InputSlot<float>();
        
        [Input(Guid = "E7FAC507-AD85-48F4-89D3-76600FF519EC")]
        public readonly InputSlot<float> Decay = new InputSlot<float>();
    }
}

