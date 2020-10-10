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

        [Input(Guid = "0a8776c5-eca9-4d8a-9eca-61fee6d8d5ac")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> PointsBuffer1 = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "cc442161-e9ca-40ea-be3b-f87189d4e155")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "414c8045-5086-4449-9d9a-03f28c3966b3")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "850e3a32-11ba-4ad2-a2b1-6164f077ddd6")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();
    }
}

