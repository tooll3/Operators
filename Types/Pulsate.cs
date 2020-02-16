using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ffed6f9e_2495_4cf3_9cda_740ecec75d10
{
    public class Pulsate : Instance<Pulsate>
    {
        [Output (Guid = "CB128B17-6855-440D-8539-BFD437E4193C")]
        public readonly Slot<float> Result = new Slot<float> ();

        [Output (Guid = "1FE51F05-F488-4DCB-98F3-B4321A991AFA")]
        public readonly Slot<float> Counter = new Slot<float> ();

        public Pulsate ()
        {
            Result.UpdateAction = Update;
            Counter.UpdateAction = Update;
        }

        private void Update (EvaluationContext context)
        {
            var beatTime = BeatTime.GetValue (context);
            var frequency = Frequency.GetValue (context);
            var intensity = Intensity.GetValue (context);

            float v = 0;
            // Off
            if (frequency == 0)
            {
                v = 0;
            }
            else if (frequency < 0.1)
            {
                v = 1 / 16f;
            }
            else if (frequency < 0.2)
            {
                v = 1 / 8f;
            }
            else if (frequency < 0.3)
            {
                v = 1 / 4f;
            }
            else if (frequency < 0.5)
            {
                v = 1;
            }
            else if (frequency < 0.7)
            {
                v = 2;
            }

            else if (frequency < 0.90)
            {
                v = 4;
            }
            else
            {
                v = 16;
            }

            Result.Value = (1 - (beatTime * v) % 1) * intensity;
            Counter.Value = (int) (beatTime * v);
        }

        [Input (Guid = "3B60DB67-3A12-44C3-91BA-5517F74879D6")]
        public readonly InputSlot<float> BeatTime = new InputSlot<float> ();

        [Input (Guid = "C1E5CE4C-6780-414E-9596-703FA7CB0392")]
        public readonly InputSlot<float> Frequency = new InputSlot<float> ();

        [Input (Guid = "399C783C-7DB2-4173-87C6-FFC2BB9CC859")]
        public readonly InputSlot<float> Intensity = new InputSlot<float> ();

    }
}