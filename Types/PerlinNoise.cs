using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class PerlinNoise : Instance<PerlinNoise>
    {
        [Output(Guid = "4a62f8ae-cb15-4e63-ad8d-749bdf24982c")]
        public readonly Slot<float> Result = new Slot<float>();

        public PerlinNoise()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var value = Value.GetValue(context);
            var seed = Seed.GetValue(context);
            var period = Period.GetValue(context);
            var octaves = Octaves.GetValue(context);
            var zoom = Zoom.GetValue(context);
            var rangeMin = RangeMin.GetValue(context);
            var rangeMax = RangeMax.GetValue(context);

            var noiseSum = 0.0f;

            for (var a = 0; a < octaves - 1; a++)
            {
                var frequency = (float)Math.Pow(2, a);
                var amplitude = (float)Math.Pow(period, a);

                var v = value * frequency / zoom + seed * 12.468f;

                noiseSum += Lerp(Noise((int)v, seed),
                                 Noise((int)v + 1, seed),
                                 Fade(v - (float)Math.Floor(v))
                                ) * amplitude;
            }

            Result.Value = (noiseSum + 1f) * 0.5f * (rangeMax - rangeMin) + rangeMin;
        }

        private static float Noise(int x, int seed)
        {
            int n = x + seed * 137;
            n = (n << 13) ^ n;
            return (float)(1.0 - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        private static float Lerp(float a, float b, float t)
        {
            return a + t * (b - a);
        }

        private static float Fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        [Input(Guid = "eabbaf77-5f74-4303-9453-6fa44facc5db")]
        public readonly InputSlot<float> Value = new InputSlot<float>();

        [Input(Guid = "bd43ee20-1ff1-4c49-ac87-87ca4a1fe66f")]
        public readonly InputSlot<int> Seed = new InputSlot<int>();

        [Input(Guid = "B7434932-AEEA-407E-BB00-22337A21F293")]
        public readonly InputSlot<float> Period = new InputSlot<float>();

        [Input(Guid = "C6286F1C-00A3-40AF-94DD-66375ED0343F")]
        public readonly InputSlot<int> Octaves = new InputSlot<int>();

        [Input(Guid = "A17F04F8-D8DE-4EEB-B4F6-7B3FA4CAD03C")]
        public readonly InputSlot<float> Zoom = new InputSlot<float>();

        [Input(Guid = "B112705E-3EC3-4904-B978-BC784D9B2F94")]
        public readonly InputSlot<float> RangeMin = new InputSlot<float>();

        [Input(Guid = "557AE817-EC36-4866-8FED-64490E9255BE")]
        public readonly InputSlot<float> RangeMax = new InputSlot<float>();
    }
}