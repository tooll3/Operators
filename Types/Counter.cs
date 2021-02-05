using System;
using System.Diagnostics;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_11882635_4757_4cac_a024_70bb4e8b504c
{
    public class Counter : Instance<Counter>
    {
        [Output(Guid = "c53e3a03-3a6d-4547-abbf-7901b5045539", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<float> Result = new Slot<float>();

        [Output(Guid = "618a3395-2609-4d73-a09f-36f2316ae612")]
        public readonly Slot<float> MovingSum = new Slot<float>();

        public Counter()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var startPosition = StartValue.GetValue(context);
            var modulo = Modulo.GetValue(context);
            var increment = Increment.GetValue(context);
            _rate = Rate.GetValue(context);
            _blending = Blending.GetValue(context);
            var reset = TriggerReset.GetValue(context);
            var jump = TriggerCount.GetValue(context);
            //var jump = false;

            if (!_initialized || reset || float.IsNaN(_count))
            {
                _count = 0;
                _initialized = true;
                jump = true;
            }

            _beatTime = EvaluationContext.BeatTime;

            if (UseRate)
            {
                var activationIndex = (int)(_beatTime * _rate);
                if (activationIndex != _lastActivationIndex)
                {
                    //Log.Debug($"ai {activationIndex}  != {_lastActivationIndex}  rate={_rate} t = {_beatTime} ");
                    _lastActivationIndex = activationIndex;
                    jump = true;
                }
            }

            if (jump)
            {
                if (modulo > 0.001f)
                {
                    _jumpStartOffset = _jumpTargetOffset;
                    _jumpTargetOffset +=  1;
                }
                else
                {
                    _jumpStartOffset = _count;
                    _jumpTargetOffset = _count + increment;
                }

                // if (_jumpTargetOffset > modulo)
                // {
                //     _count = 0;
                //     _jumpStartOffset = 0;
                //     _jumpTargetOffset = increment;
                // }
                _lastJumpTime = _beatTime; 
            }

            if (_blending >= 0.001)
            {
                var t = (Fragment / _blending).Clamp(0,1);
                if (SmoothBlending.GetValue(context))
                    t = MathUtils.SmootherStep(0, 1, t);

                _count = MathUtils.Lerp(_jumpStartOffset, _jumpTargetOffset, t);
            }
            else
            {
                _count = _jumpTargetOffset;
            }

            if (modulo > 0.001f)
            {
                Result.Value = (_count % modulo) * increment + startPosition;
            }
            else
            {
                Result.Value = _count + startPosition;
            }
        }

        public float Fragment =>
            UseRate
                ? (float)((_beatTime - _lastJumpTime) * _rate).Clamp(0, 1)
                : (float)(_beatTime - _lastJumpTime).Clamp(0, 1);

        private bool UseRate => _rate > -1;

        private float _rate;
        private double _beatTime;
        private float _blending;

        private bool _initialized = false;
        private int _lastActivationIndex = 0;
        private double _lastJumpTime;
        private float _count;
        private float _jumpStartOffset;
        private float _jumpTargetOffset;

        // private void Update (EvaluationContext context)
        // {
        //     if (TriggerReset.GetValue(context))
        //     {
        //         _value = StartValue.GetValue(context);
        //     }
        //     
        //     
        //     var countTriggered = TriggerCount.GetValue(context);
        //     if (countTriggered != _lastCountTriggered)
        //     {
        //         if (countTriggered)
        //             _value += Increment.GetValue(context);
        //             
        //         _lastCountTriggered = countTriggered;
        //     }
        //
        //     Result.Value = _value;
        // }
        //
        // private float _value;
        // private bool _lastCountTriggered;

        [Input(Guid = "eefdb8ca-68e7-4e39-b302-22eb8930fb8c")]
        public readonly InputSlot<bool> TriggerCount = new InputSlot<bool>();

        [Input(Guid = "7BFBAE6B-FA0B-4E5A-8040-E0BE3600AFEB")]
        public readonly InputSlot<bool> TriggerReset = new InputSlot<bool>();

        [Input(Guid = "754CEBE3-AB6C-4877-9C32-C67FBAE9E4C2")]
        public readonly InputSlot<float> StartValue = new InputSlot<float>();

        [Input(Guid = "BCA3F7B2-A093-4CB3-89A5-0E2681760607")]
        public readonly InputSlot<float> Increment = new InputSlot<float>();

        [Input(Guid = "73B493CB-91D1-4D4F-B9A8-005017ECAC8F")]
        public readonly InputSlot<float> Modulo = new InputSlot<float>();

        [Input(Guid = "286CBBFB-796D-499F-93D3-D467512110BE")]
        public readonly InputSlot<float> Rate = new InputSlot<float>();

        [Input(Guid = "B04D475B-A898-421B-BF26-AE5CF982A351")]
        public readonly InputSlot<float> Blending = new InputSlot<float>();

        [Input(Guid = "E0C386B9-A987-4D11-9171-2971FA759827")]
        public readonly InputSlot<bool> SmoothBlending = new InputSlot<bool>();
    }
}