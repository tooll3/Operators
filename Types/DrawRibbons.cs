using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_37a747b0_ec0e_4ebc_83dd_2e03022ad100
{
    public class DrawRibbons : Instance<DrawRibbons>
    {
        [Output(Guid = "324f8114-dae9-4cc8-aa88-021d84dbaf79")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "bbec658b-84fa-4528-ad03-6b306531b152")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "22a23dbc-0222-441d-8435-b630dcd77acb")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "5124bd22-a688-4adf-b22b-3a212b1d272a")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "979e0561-2dfb-4188-b86f-c99823e87fc7")]
        public readonly InputSlot<float> FogRate = new InputSlot<float>();

        [Input(Guid = "2fe3a9e9-037a-4c7d-bbe3-011045807f85")]
        public readonly InputSlot<float> FogBias = new InputSlot<float>();

        [Input(Guid = "dc7ddc1f-cbb4-4ab1-9c06-8325ad5f43c3")]
        public readonly InputSlot<System.Numerics.Vector4> FogColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "3d9b1ffc-029a-43a2-85da-1be29d2a25da")]
        public readonly InputSlot<float> ShrinkWithDistance = new InputSlot<float>();

        [Input(Guid = "a7fdbfcb-5477-484b-b305-2a6ba2f5b3a4")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "5124b85d-5c09-4329-bf33-ef3cc13f30aa")]
        public readonly InputSlot<int> BlendMod = new InputSlot<int>();

        [Input(Guid = "1ce27f43-3664-44e6-9a0b-5fcca3a5b9fe")]
        public readonly InputSlot<bool> EnableDepthWrite = new InputSlot<bool>();
    }
}

