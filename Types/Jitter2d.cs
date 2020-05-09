using System;
using System.Numerics;
using System.Security.Claims;
using System.Security.Policy;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_23794a1f_372d_484b_ac31_9470d0e77819
{
    public class Jitter2d : Instance<Jitter2d>
    {
        [Output(Guid = "4f1fa28e-f010-48d5-bef1-51bceac17649", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<Vector2> NewPosition = new Slot<Vector2>();

        public Jitter2d()
        {
            NewPosition.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var startPosition = Position.GetValue(context);
            var limitRange = MaxRange.GetValue(context);
            var seed = Seed.GetValue(context);
            var jumpDistance = JumpDistance.GetValue(context);
            var frequency = Frequency.GetValue(context);
            var smoothing = Smoothing.GetValue(context);
            var reset = Reset.GetValue(context);
            var jump = Jump.GetValue(context);

            if (!_initialized || reset || float.IsNaN(_offset.X) || float.IsNaN(_offset.Y))
            {
                _random = new Random(seed);
                _offset = Vector2.Zero;
                _initialized = true;
                jump = true;
            }

            var beatTime = EvaluationContext.BeatTime;
            var useFrequency = frequency > 0.0001f;
            if(useFrequency) {
                var activationIndex = (int)(beatTime * frequency);
                if (activationIndex != _lastActivationIndex)
                {
                    _lastActivationIndex = activationIndex;
                    jump = true;
                }
            }

            if (jump)
            {
                _jumpStartOffset = _offset;
                _jumpTargetOffset = _offset + new Vector2(
                                                          (float)((_random.NextDouble() - 0.5f) * jumpDistance * 2f),
                                                          (float)((_random.NextDouble() - 0.5f) * jumpDistance * 2f));

                if (limitRange > 0.001f)
                {
                    var d = _jumpTargetOffset.Length();
                    if (d > limitRange)
                    {
                        //var overshot = Math.Min(d, limitRange) / 2;
                        var overshot =  Math.Min(d- limitRange, limitRange);
                        var random = _random.NextDouble() * overshot;
                        var distanceWithinLimit = limitRange - (float)random;
                        //var overshot = 0.1f;
                        //var moveTowardsCenter = -_jumpTargetOffset * overshot;
                        var normalized = _jumpTargetOffset / d;
                        _jumpTargetOffset =  normalized * distanceWithinLimit;
                    }
                }

                _lastJumpTime = beatTime;
            }

            if (smoothing >= 0.001)
            {
                //var useFrequencyRelevantTiming = frequency > 0.001f;

                var t = useFrequency
                            ? (float)((beatTime - _lastJumpTime) * frequency / smoothing ).Clamp(0,1)
                            : (float)((beatTime - _lastJumpTime) / smoothing);
                
                var tt = MathUtils.SmootherStep(0, 1, t);
                _offset = Lerp(_jumpStartOffset, _jumpTargetOffset, tt);
            }
            else
            {
                _offset = _jumpTargetOffset;
            }

            NewPosition.Value = _offset + startPosition;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        private Random _random = new Random();
        private bool _initialized = false;
        private int _lastActivationIndex = 0;
        private double _lastJumpTime;

        private Vector2 _offset;
        private Vector2 _jumpStartOffset;
        private Vector2 _jumpTargetOffset;

        [Input(Guid = "8F17B67C-2D55-4148-B880-1DD948CE9808")]
        public readonly InputSlot<Vector2> Position = new InputSlot<Vector2>();
        
        [Input(Guid = "F101AF0C-DE31-4AFB-ACB4-8166C62C2EC8")]
        public readonly InputSlot<float> JumpDistance = new InputSlot<float>();

        [Input(Guid = "87DE02D8-A7AF-4E5C-A079-A70AF222F0BE")]
        public readonly InputSlot<float> MaxRange = new InputSlot<float>();

        [Input(Guid = "1DF95BEB-DA6D-4263-8273-7A180FD190F5")]
        public readonly InputSlot<float> Frequency = new InputSlot<float>();

        [Input(Guid = "38086D8A-15E0-4F3E-B161-A46A79FC5CC3")]
        public readonly InputSlot<float> Smoothing = new InputSlot<float>();
        
        [Input(Guid = "34EA227B-13DB-42DD-ADE5-1B07D2F6BAD5")]
        public readonly InputSlot<int> Seed = new InputSlot<int>();
        
        [Input(Guid = "BF5E7465-7349-48BD-8358-CCE6E9983AA0")]
        public readonly InputSlot<bool> Reset = new InputSlot<bool>();

        [Input(Guid = "CE65293A-E13E-427A-93D2-EFB0214AD274")]
        public readonly InputSlot<bool> Jump = new InputSlot<bool>();
    }
}