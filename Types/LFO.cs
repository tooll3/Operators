using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using T3.Operators.Types.Id_58aa74af_32aa_4c46_8bb5_5811f16bf7f8;
using T3.Operators.Types.Id_c3a18346_930c_4242_9e42_aa9b3a439395;






















namespace T3.Operators.Types.Id_c5e39c67_256f_4cb9_a635_b62a0d9c796c
{
    public class LFO : Instance<LFO>
    {
        [Output(Guid = "c47e8843-6e8d-4eaf-a554-874b3af9ee63", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<float> Result = new Slot<float>();

        public LFO()
        {
            Result.UpdateAction = Update;
        }
        
        

        private void Update(EvaluationContext context)
        {

            
            var time = OverrideTime.IsConnected
                           ? OverrideTime.GetValue(context)
                           : EvaluationContext.BeatTime;

            var rate = Rate.GetValue(context);
            var phase = Phase.GetValue(context);
            
            var fraction =  (float)(((time * rate + phase ) % 1f / Ratio.GetValue(context)).Clamp(0f,1f));

            var bias = Bias.GetValue(context);
            float r = fraction;
            switch ((Shapes)Shape.GetValue(context))
            {
                case Shapes.Ramp:
                    r = fraction;
                    break;
                case Shapes.Saw:
                    r = 1 - fraction;
                    break;
                case Shapes.Sine:
                    r = (float)Math.Sin(fraction * 2 * 3.141578f)/2 + 0.5f;
                    break;
                case Shapes.Square:
                    r = fraction > 0.5f ? 1 : 0;
                    break;
                case Shapes.ZigZag:
                    r = fraction < 0.5f
                            ? (fraction * 2)
                            : (1-(fraction - 0.5f) * 2);  
                    break;

            }
            
            var biased = bias >= 0 
                             ? (float)Math.Pow(r, bias+1)
                             : 1-(float)Math.Pow(1-r, 1-bias);    
            
            Result.Value = biased * Amplitude.GetValue(context) + Offset.GetValue(context);
        }


        private enum Shapes
        {
            Ramp,
            Saw,
            Sine,
            Square,
            ZigZag,
        }

        [Input(Guid = "4C38C34C-D992-47F1-BCB5-9BD13FC6474B", MappedType = typeof(Shapes))]
        public readonly InputSlot<int> Shape = new InputSlot<int>();



        [Input(Guid = "a4d48d80-936c-4bbb-a2e8-32f86edd4ab2")]
        public readonly InputSlot<float> Rate = new InputSlot<float>();

        [Input(Guid = "3b9e7272-ccf3-4fff-a079-5fcbb8a6c7d5")]
        public readonly InputSlot<float> Ratio = new InputSlot<float>();


        [Input(Guid = "36ae5b4b-62e9-49c0-b841-97394122cb1e")]
        public readonly InputSlot<float> Phase = new InputSlot<float>();
        
        [Input(Guid = "8a5033c2-7d22-44d7-9472-d23677b11388")]
        public readonly InputSlot<float> Amplitude = new InputSlot<float>();
        
        [Input(Guid = "126511E6-771D-4DD0-8A9D-1861C7B45D23")]
        public readonly InputSlot<float> Offset = new InputSlot<float>();
        
        [Input(Guid = "3396DE1F-03AF-43EE-A43A-55016BEC70AE")]
        public readonly InputSlot<float> Bias = new InputSlot<float>();

        [Input(Guid = "76ca8a8b-f252-4687-805e-fb7a86a16567")]
        public readonly InputSlot<float> OverrideTime = new InputSlot<float>();

    }
}