using System;
using T3.Core.Operator;

namespace T3.Operators.Types
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
    }
}

