using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2c3d2c26_ac45_42e9_8f13_6ea338333568
{
    public class LinearRamp : Instance<LinearRamp>
    {
        [Output(Guid = "d140f068-d71e-4af5-a563-ab599dae5dbf")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "d6e157fb-5300-4a9a-aea8-8b0ea0104ea3")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "4a54d674-cd99-49bd-b079-061b26a44ed5")]
        public readonly InputSlot<System.Numerics.Vector4> Fill = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "2c095b96-a489-43cd-90d3-009661f28c8b")]
        public readonly InputSlot<System.Numerics.Vector4> Background = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "abf3456d-35bc-49ec-9aa6-c5571fbb209a")]
        public readonly InputSlot<System.Numerics.Vector2> Center = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "10d59d0f-a5a3-42e6-b874-345ab028978e")]
        public readonly InputSlot<float> Width = new InputSlot<float>();

        [Input(Guid = "8169be8f-cb35-4900-b462-f2139b412d59")]
        public readonly InputSlot<float> Rotation = new InputSlot<float>();

        [Input(Guid = "d9d6878b-29d4-40d9-b342-ed8f60178ec2")]
        public readonly InputSlot<float> PingPong = new InputSlot<float>();

        [Input(Guid = "b2fe829e-10cb-46f3-95f2-1e9acb3bd052")]
        public readonly InputSlot<float> SmoothGradient = new InputSlot<float>();

        [Input(Guid = "53afc8d9-f417-4628-9a97-220bec62919f")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();
    }
}

