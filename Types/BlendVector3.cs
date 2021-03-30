using System;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_fc201df2_8b05_4567_9f24_0d9128aa8507
{
    public class BlendVector3 : Instance<BlendVector3>
    {
        [Output(Guid = "93217299-1c47-4afd-b9ef-40647df308f9")]
        public readonly Slot<float> Result = new Slot<float>();

        public BlendVector3()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = 0;

            var collectedTypedInputs = Values.GetCollectedTypedInputs();
            var count = collectedTypedInputs.Count;
            if (count == 0)
                return;
            
            var f = F.GetValue(context);
            
            var index1 = (int)MathUtils.Fmod((int)f, count);
            var index2 = (int)MathUtils.Fmod((int)(f+1), count);
            var mix = MathUtils.Fmod(f, 1);

            Result.Value = MathUtils.Lerp(collectedTypedInputs[index1].GetValue(context),
                                          collectedTypedInputs[index2].GetValue(context),
                                          mix);
            
        }
        
         

        [Input(Guid = "970b9fb2-0dff-41c8-9e49-b51b5f37e99f")]
        public readonly MultiInputSlot<float> Values = new MultiInputSlot<float>();
        
        [Input(Guid = "f5f12cf3-5750-4a3c-807e-9da29f950c29")]
        public readonly InputSlot<float> F = new InputSlot<float>();

    }
}