using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b89c5923_cc58_4d7a_8dce_eb1f8cdfc6e8
{
    public class RenderQuad : Instance<RenderQuad>
    {
        [Output(Guid = "1ff16183-13b9-4c8f-a82a-77e8be0c641b")]
        public readonly Slot<T3.Core.Command> Output = new Slot<T3.Core.Command>();


        [Input(Guid = "018dab29-db3b-49ee-872b-9042c4c0bced")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "105d474f-a54e-4350-8de6-8bfd4dc0b0dd")]
        public readonly InputSlot<float> Width = new InputSlot<float>();

        [Input(Guid = "a2d39e5b-38c7-4751-bc29-7389f9e9d8e5")]
        public readonly InputSlot<float> Height = new InputSlot<float>();

        [Input(Guid = "307e2b98-a337-4636-969f-a19841b11511")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();
    }
}

