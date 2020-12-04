using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_da93f7d1_ef91_4b4a_9708_2d9b1baa4c14
{
    public class RemapColor : Instance<RemapColor>
    {
        [Output(Guid = "16e37306-05e1-4de6-babd-80a8d1472a2f")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "876f6f64-7cb4-4060-8571-e0b78b437d41")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "dc3b3322-f124-4172-8b55-bed131d4faca")]
        public readonly InputSlot<System.Numerics.Vector2> Center = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "e9a54487-dad0-4508-a2a2-2fce1ee2f1aa")]
        public readonly InputSlot<float> Width = new InputSlot<float>();

        [Input(Guid = "d98ab4ae-8d98-4228-872a-d99d72004892")]
        public readonly InputSlot<float> Rotation = new InputSlot<float>();

        [Input(Guid = "5dd50849-1f61-4534-85e5-44e51db4974b")]
        public readonly InputSlot<bool> PingPong = new InputSlot<bool>();

        [Input(Guid = "86a565c0-257b-4b64-8112-2abb04dd1a11")]
        public readonly InputSlot<bool> Repeat = new InputSlot<bool>();

        [Input(Guid = "cb52ff49-17de-4e36-b918-5de6973a234a")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "f10896b4-b8fa-4e16-a7ec-4a641ddda926")]
        public readonly InputSlot<float> Bias = new InputSlot<float>();

        [Input(Guid = "c45d487b-3221-44c7-bf9e-b982a65280f6")]
        public readonly InputSlot<T3.Core.DataTypes.Gradient> Gradient = new InputSlot<T3.Core.DataTypes.Gradient>();
    }
}

