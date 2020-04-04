using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_946da16c_f536_4887_b764_af9468f22c0f
{
    public class Blur : Instance<Blur>
    {
        [Output(Guid = "fa46b9f0-46d6-4ab3-8406-409e1dc5e9a4")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "c115fd60-86c5-425f-975b-0b5e92c0f42b")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "99188668-b6ac-468b-a892-cd020a3862b2")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "3c8b43be-430f-4afe-8244-5282be49bfbc")]
        public readonly InputSlot<float> Samples = new InputSlot<float>();

        [Input(Guid = "03e6c20c-6b8a-422e-bba1-1cefddc645fd")]
        public readonly InputSlot<float> Offset = new InputSlot<float>();

        [Input(Guid = "d1421b9f-ddde-426a-9d68-32d3a41bf881")]
        public readonly InputSlot<float> Glow = new InputSlot<float>();

        [Input(Guid = "e4e5e654-d570-4dea-ad16-f4eb1018ff2f")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();
    }
}

