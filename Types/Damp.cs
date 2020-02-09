using System;
using T3.Core.Operator;

namespace T3.Operators.Types.Id_af9c5db8_7144_4164_b605_b287aaf71bf6
{
    public class Damp : Instance<Damp>
    {
        [Output(Guid = "aacea92a-c166-46dc-b775-d28baf9820f5")]
        public readonly Slot<float> Result = new Slot<float>();

        public Damp()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var v = Value.GetValue(context);
            var friction = Friction.GetValue(context);

            var dt = (float)(context.TimeInBars - _lastTime);
            _lastTime = context.TimeInBars;
            dt = 0.015f;    // hack until we have beattime in context
            
            var f = friction * dt;
            f = Math.Max(0, f);
            f = Math.Min(1, f);
            _dampedValue = v * f + (1 - f) * _dampedValue;
            Result.Value = _dampedValue;
        }

        private double _lastTime;
        private float _dampedValue;
        
        [Input(Guid = "795aca79-dd10-4f28-a290-a30e7b27b436")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "F29D5426-5E31-4C7C-BE77-5E45BFB9DAA9")]
        public readonly InputSlot<float> Friction = new InputSlot<float>();
        
    }
}
