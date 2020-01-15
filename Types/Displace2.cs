using System;
using T3.Core.Operator;

namespace T3.Operators.Types.Id_1b149f1f_529c_4418_ac9d_3871f24a9e38
{
    public class Displace2 : Instance<Displace2>
    {
        [Output(Guid = "0faa056c-b1d6-4e1f-a9be-b0791f3bae84")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> Output = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "d0508dfa-89cf-4713-8f5e-893dd5bfc3f4")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> ImageA = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "3b5b278d-fd4e-4216-9916-5cd7ffd54ab2")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> ImageB = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "c13b83ce-7dd2-4b4e-bf19-b994493b92a3")]
        public readonly InputSlot<float> SampleRadius = new InputSlot<float>();

        [Input(Guid = "0f2867ab-a65e-4bf3-b1b5-9c241690ba5f")]
        public readonly InputSlot<float> Displacement = new InputSlot<float>();

        [Input(Guid = "0eff3a75-eafc-4a5e-8a2c-10577c12e776")]
        public readonly InputSlot<float> DisplacementOffset = new InputSlot<float>();

        [Input(Guid = "695d209a-cd9f-4d2e-b153-da1811ac6e70")]
        public readonly MultiInputSlot<float> Samples = new MultiInputSlot<float>();

        [Input(Guid = "985913e0-f0cf-498d-930d-2d70e6837f25")]
        public readonly InputSlot<float> ShiftX = new InputSlot<float>();

        [Input(Guid = "2d74b10e-569e-4357-938a-145c8db33957")]
        public readonly InputSlot<float> ShiftY = new InputSlot<float>();

        [Input(Guid = "dc8dfa33-1a49-4800-8c1f-89b29d7427f3")]
        public readonly InputSlot<float> Angle = new InputSlot<float>();

    }
}

