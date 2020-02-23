using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_1fec51bc_4de7_400a_8910_db39f4129579
{
    public class NumberPattern : Instance<NumberPattern>
    {
        [Output(Guid = "569ef449-6919-4e6f-880e-6f26c6fd2a5e")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "8a3681fa-4820-4855-9a12-c3740772f4d8")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "22fcd65b-e2a2-4586-b0d0-ab4c0b0f2551")]
        public readonly InputSlot<System.Numerics.Vector4> ColorA = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "5047d175-4d55-4784-a319-d4e1959fa385")]
        public readonly InputSlot<System.Numerics.Vector4> ColorB = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "d4adfa55-97d8-4c42-a2fc-4c89f6c43c8f")]
        public readonly InputSlot<float> Offset = new InputSlot<float>();

        [Input(Guid = "9541be0a-9226-4f06-8873-0eb45f81cc24")]
        public readonly InputSlot<System.Numerics.Vector2> Size = new InputSlot<System.Numerics.Vector2>();
    }
}

