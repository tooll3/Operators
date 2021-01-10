using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_42cb88bc_beb8_4d89_ac99_44b77be5f03e
{
    public class ReplicateAtPoints : Instance<ReplicateAtPoints>
    {
        [Output(Guid = "774a96e4-24e2-4e1a-a70d-63794d24dd51")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "408ae7c7-9aa8-4537-8c55-b5689f8f9b56")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "ba7befdf-270b-4ac0-bfc2-7543e2c3097b")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "0af05ab4-0d77-4f01-a79b-691f58f702ef")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "cc20eb7e-8522-4da0-8459-5cb800607d16")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture_ = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "dd511aab-0472-4109-9c10-cc1ab5042499")]
        public readonly InputSlot<bool> EnableZWrite = new InputSlot<bool>();

        [Input(Guid = "70f4cc27-f901-4faa-aa2e-b4cd2a50ff73")]
        public readonly InputSlot<bool> EnableZTest = new InputSlot<bool>();

        [Input(Guid = "6c36bf68-e22f-419d-9ec0-f60a83d6a560")]
        public readonly InputSlot<int> BlendMode = new InputSlot<int>();

        [Input(Guid = "bfdbe2b3-080e-4c63-9c90-8216041911ab")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> GeometryBuffer = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();
    }
}

