using System;
using System.Text;
using SharpDX;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_bf76bc78_74e1_45c3_9c67_de50262a48ae 
{
    public class ScrambleBuffer : Instance<ScrambleBuffer>
    {
        [Output(Guid = "f460db31-2603-4468-ac68-d1a3b93c41da")]
        public readonly Slot<System.Text.StringBuilder> Builder = new Slot<System.Text.StringBuilder>();

        
        public ScrambleBuffer()
        {
            Builder.UpdateAction = Update;
            Builder.DirtyFlag.Trigger |= DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var stringBuilder = InputBuffer.GetValue(context);
            if (stringBuilder == null)
                return;
            
            //if (stringBuilder.Length < MaxLength.GetValue(context))
            //    stringBuilder.Append(String.GetValue(context));
            var bufferLength = stringBuilder.Length;

            var maxRandomLength = MaxLength.GetValue(context);
            if (maxRandomLength < 0)
                maxRandomLength = 0;
            var lenRemove = (int)_random.NextLong(0, maxRandomLength);

            var maxRandPos = bufferLength - lenRemove;
            if (maxRandPos < 0)
                maxRandPos = 0;
            
            var randPos = (int)_random.NextLong(0, maxRandPos);
            if (lenRemove > 0 &&  lenRemove < bufferLength)
                stringBuilder.Remove(randPos, lenRemove);
            
            Builder.Value = stringBuilder;
        }

        private Random _random = new Random();
        
        [Input(Guid = "c80a289a-e0d5-459f-b79e-fff72757b416")]
        public readonly InputSlot<StringBuilder> InputBuffer = new InputSlot<StringBuilder>();

        [Input(Guid = "f35cd93e-ed9a-472c-b03f-ff3cabcbc631")]
        public readonly InputSlot<string> String = new InputSlot<string>();

        [Input(Guid = "7e68d5fd-f9be-4f23-bc1b-0bdb72e63137")]
        public readonly InputSlot<int> MaxLength = new InputSlot<int>();

    }
}
