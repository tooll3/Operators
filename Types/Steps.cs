using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_4cdc0f90_6ce9_4a03_9cd0_efeddee70567
{
    public class Steps : Instance<Steps>
    {
        [Output(Guid = "b2c389a0-6f8c-4e64-b3d5-09b549ae32c1")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "3fa998ee-becf-4a32-948f-5c5be67d7728")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "c5c7888a-294d-4a51-a68d-446cc7f1444c")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "dc99dcb5-481e-4b43-afdf-ad6d318ed24f")]
        public readonly InputSlot<float> Copies = new InputSlot<float>();

        [Input(Guid = "85d5e801-f9f2-41d6-9d7b-3fbfda781fca")]
        public readonly InputSlot<float> Bias = new InputSlot<float>();

        [Input(Guid = "4ceccd98-d61e-4870-9b57-1c9ca84f3e23")]
        public readonly InputSlot<float> Offset = new InputSlot<float>();

        [Input(Guid = "7ccb8243-a6c6-4d95-a667-cb4abd396caf")]
        public readonly InputSlot<float> SmoothRadius = new InputSlot<float>();

        [Input(Guid = "4326b1bd-6db5-4345-a91e-f7b70cff92b3")]
        public readonly InputSlot<float> Rotate = new InputSlot<float>();

        [Input(Guid = "5aaeb706-8d19-4c00-b737-474be58a92ed")]
        public readonly InputSlot<float> LineWidth = new InputSlot<float>();

        [Input(Guid = "2be0aa4a-f599-418f-866d-967c361908a3")]
        public readonly InputSlot<float> Fade = new InputSlot<float>();

        [Input(Guid = "c550629c-b200-4c66-954f-d1d50ef5c542")]
        public readonly InputSlot<T3.Core.DataTypes.Gradient> Ramp = new InputSlot<T3.Core.DataTypes.Gradient>();

        [Input(Guid = "82c7d336-9a0b-40e0-938c-9e2007019b82")]
        public readonly InputSlot<T3.Core.DataTypes.Gradient> Edge = new InputSlot<T3.Core.DataTypes.Gradient>();
    }
}

