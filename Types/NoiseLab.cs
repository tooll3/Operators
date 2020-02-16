using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_97eb35ec_2825_4f63_8fdf_3fe38fa9e652
{
    public class NoiseLab : Instance<NoiseLab>
    {
        [Output(Guid = "bfce8bf6-9ef3-4fdb-8c6e-21aa65485f14")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "ab0d389b-4cd3-4781-8c35-9ae59ccae564")]
        public readonly InputSlot<System.Numerics.Vector2> Size = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "6fe3a975-6bb5-4cf1-9a24-6cf04ac800b6")]
        public readonly InputSlot<System.Numerics.Vector4> ColorA = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "a966d9a1-b36f-4f85-b918-8651ea7d055d")]
        public readonly InputSlot<System.Numerics.Vector4> ColorB = new InputSlot<System.Numerics.Vector4>();
    }
}

