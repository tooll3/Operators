using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_836f211f_b387_417c_8316_658e0dc6e117
{
    public class DrawLines : Instance<DrawLines>
    {
        [Output(Guid = "73ebf863-ba71-421c-bee7-312f13c5eff0")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "75419a73-8a3e-4538-9a1d-e3b0ce7f8561")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "df158fcf-3042-48cf-8383-7bf4c1bcb8a6")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "c10f9c6c-9923-42c6-848d-6b98097acc67")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "13f33c50-ebf1-44ef-81b8-823ce5d74784")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> PointsGBuffer = new InputSlot<SharpDX.Direct3D11.Buffer>();
    }
}

