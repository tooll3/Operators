using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_6820b166_1782_43b9_bc5c_6b4f63b16f86
{
    public class FakeLight : Instance<FakeLight>
    {
        [Output(Guid = "27e1e1b6-89e0-4dca-98e1-5989286f6331")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> Output = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "00c53b57-7347-4ebc-97d7-1ab48483f09b")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> HeightMap = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "56eccacb-65ac-4813-ad7e-fad8e581f570")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> LightMap = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "0212bfb2-9f5f-4d60-aab0-3f9525bd7bfc")]
        public readonly InputSlot<float> Impact = new InputSlot<float>();

        [Input(Guid = "4aa128ab-d0a8-42d5-800f-6992959bd0cf")]
        public readonly InputSlot<float> Shade = new InputSlot<float>();

        [Input(Guid = "7f0c127b-ee60-44c8-8490-2d3599cde4a2")]
        public readonly InputSlot<float> Twist = new InputSlot<float>();

        [Input(Guid = "f93db0c6-c5ed-40da-9677-0c284618f5bb")]
        public readonly InputSlot<float> SampleRadius = new InputSlot<float>();

        [Input(Guid = "03298545-a5d6-44d5-bb7c-4747172d2667")]
        public readonly InputSlot<System.Numerics.Vector2> Direction = new InputSlot<System.Numerics.Vector2>();
    }
}

