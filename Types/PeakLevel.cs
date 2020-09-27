using System;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_d3fb5baf_43f8_4983_a1d9_42f4005a3af0
{
    public class PeakLevel : Instance<PeakLevel>
    {
        [Output(Guid = "6fe37109-0177-4823-9466-eaa49adb19d4")]
        public readonly Slot<float> Result = new Slot<float>();

        [Output(Guid = "79BADB66-ED5C-4E01-B26A-29B5AA115FC4")]
        public readonly Slot<float> MovingSum = new Slot<float>();

        [Output(Guid = "80DCAD3B-5E93-4991-855D-24176EC54F4D", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<bool> FoundPeak = new Slot<bool>();
        
        public PeakLevel()
        {
            Result.UpdateAction = Update;
            MovingSum.UpdateAction = Update;
            FoundPeak.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var value = Value.GetValue(context);
            //FoundPeak.Value = false;
            
            // if (float.IsNaN(_lastPeak))
            // {
            //     _lastPeak = 0;
            // }
            // if (value > _lastPeak + Threshold.GetValue(context))
            // {
            //     _lastPeak = value;
            // }
            // else
            // {
            //     _lastPeak *= Decay.GetValue(context);
            // }
            var decayRate =  Decay.GetValue(context);
            var minTimeBetweenBeats = MinTimeBetweenPeaks.GetValue(context);
            _averageLevel = MathUtils.Lerp(value, _averageLevel,  0.96f).Clamp(0.001f, 1000);    // very long smoothing to normalize level
            
            var normalizedValue = value / _averageLevel /3;
            _energy +=  (float)Math.Pow(normalizedValue,3);

            var timeSinceLastPeak = EvaluationContext.BeatTime - _lastPeakTime;
            if (timeSinceLastPeak < 0)
            {
                _lastPeakTime = Double.NegativeInfinity;
                
            }
            if (_energy > Threshold.GetValue(context)  && timeSinceLastPeak > minTimeBetweenBeats)
            {
                _lastPeakTime = EvaluationContext.BeatTime;
                _energy = 0; 
                FoundPeak.Value = true;
            }
            else
            {
                _energy = (_energy - (float)EvaluationContext.LastFrameDuration * decayRate ).Clamp(0,100);
                FoundPeak.Value = false;
            }

            MovingSum.Value = _energy;
            Result.Value = _lastPeak;
        }

        private float _lastPeak;
        private float _averageLevel;
        private float _energy;
        private double _lastPeakTime = double.NegativeInfinity;
        
        

        [Input(Guid = "88ed25d3-ab67-47da-ad38-2f0126ce0492")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "279C4F32-2F8A-4679-AFE4-BBF14CF6BA05")]
        public readonly InputSlot<float> Threshold = new InputSlot<float>();
        
        [Input(Guid = "30f474d1-5c5d-4e39-a31a-b4554809eae0")]
        public readonly InputSlot<float> Decay = new InputSlot<float>();

        [Input(Guid = "D38D54B4-D15C-40C3-A5F1-6546F444965C")]
        public readonly InputSlot<float> MinTimeBetweenPeaks = new InputSlot<float>();

    }
}