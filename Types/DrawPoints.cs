using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ffd94e5a_bc98_4e70_84d8_cce831e6925f
{
    public class DrawPoints : Instance<DrawPoints>
    {
        [Output(Guid = "b73347d9-9d9f-4929-b9df-e2d6db722856")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "b2c16439-0a37-41c3-b532-d8a375ff1414")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "15b673ae-37b5-4d6e-bfd2-c5b9be2bdba2")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "08a8c55b-71e7-4d02-a3bd-582dd5514529")]
        public readonly InputSlot<SharpDX.Vector4[]> PointBuffer = new InputSlot<SharpDX.Vector4[]>();

        [Input(Guid = "850e3a32-11ba-4ad2-a2b1-6164f077ddd6")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();
    }
}

