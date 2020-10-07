using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2dc5c9d1_ea93_4597_a4d9_7b610aad603a
{
    public class MixGPoints : Instance<MixGPoints>
    {

        [Output(Guid = "04712196-071f-4ed6-a1a2-c1c9a4f6a94e")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> OutBuffer = new Slot<SharpDX.Direct3D11.Buffer>();

        [Input(Guid = "c5480ce5-a8ba-4a26-8cee-c28e442020b7")]
        public readonly InputSlot<int> BlendMode = new InputSlot<int>();

        [Input(Guid = "9e897461-7e6e-4d9d-a65d-2e59be77abcd")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> PointABuffer = new InputSlot<SharpDX.Direct3D11.Buffer>();

        [Input(Guid = "7628cae5-2860-43b6-876f-dfed21325680")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> PointBBuffer = new InputSlot<SharpDX.Direct3D11.Buffer>();
    }
}

