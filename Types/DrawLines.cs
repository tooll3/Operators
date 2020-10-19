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

        [Input(Guid = "e15b6dc7-aaf9-4244-a4b8-4ac13ee7d23f")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "75419a73-8a3e-4538-9a1d-e3b0ce7f8561")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "df158fcf-3042-48cf-8383-7bf4c1bcb8a6")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "850a2d9a-303f-4393-90d2-798fe2638b1c")]
        public readonly InputSlot<float> FogRate = new InputSlot<float>();

        [Input(Guid = "5ab5501c-8ec1-4c54-9c9a-1f7b66b4d9ac")]
        public readonly InputSlot<float> FogBias = new InputSlot<float>();

        [Input(Guid = "049e8514-a5a5-4a5e-806e-1acf99fcf9ad")]
        public readonly InputSlot<System.Numerics.Vector4> FogColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "d0919481-203a-4379-8094-187d6209e00d")]
        public readonly InputSlot<float> ShrinkWithDistance = new InputSlot<float>();

        [Input(Guid = "c10f9c6c-9923-42c6-848d-6b98097acc67")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "d90ff4e6-7d70-441f-a064-b40401025c36")]
        public readonly InputSlot<int> BlendMod = new InputSlot<int>();
    }
}

