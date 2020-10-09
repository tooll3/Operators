using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2dc5c9d1_ea93_4597_a4d9_7b610aad603a
{
    public class MixGPoints : Instance<MixGPoints>
    {

        [Output(Guid = "e75ebb0c-c9b2-41ec-8895-2a8cb0cf350b")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> OutBuffer2 = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "c5480ce5-a8ba-4a26-8cee-c28e442020b7")]
        public readonly InputSlot<int> BlendMode = new InputSlot<int>();

        [Input(Guid = "ba7ffda2-f9f6-440d-a174-7339844835fa")]
        public readonly InputSlot<float> BlendValue = new InputSlot<float>();

        [Input(Guid = "1e499f9a-2b08-4da0-9397-e943e766d797")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> PointsA = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "53fc0473-4785-49df-83bd-5640f03d83bc")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> PointsB = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();
    }
}

