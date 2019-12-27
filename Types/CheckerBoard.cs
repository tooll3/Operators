using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class CheckerBoard : Instance<CheckerBoard>
    {
        [Output(Guid = "9dd9dbeb-b506-4d10-97b7-34feaab91f07")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "70c4ecc7-72a2-42ee-8546-cbff2c08aa27")]
        public readonly InputSlot<System.Numerics.Vector2> Size = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "1192cfbe-585f-45f0-a37b-5fe78ca32d7b")]
        public readonly InputSlot<System.Numerics.Vector4> ColorA = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "b08aba90-f33f-4402-bb7b-bcfc4bb624ce")]
        public readonly InputSlot<System.Numerics.Vector4> ColorB = new InputSlot<System.Numerics.Vector4>();
    }
}

