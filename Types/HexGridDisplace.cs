using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ee398291_674c_409b_aab8_9ca843ce6cef
{
    public class HexGridDisplace : Instance<HexGridDisplace>
    {
        [Output(Guid = "2c2d62c4-3735-40a4-bc97-eaecf7e4cb7f")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "452fb0e1-34c1-4042-9a7c-30bf8dd4cb3c")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "dc8c94e6-97d9-4aa5-9edd-5a8d01e4f7c4")]
        public readonly InputSlot<System.Numerics.Vector4> Fill = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "165b0ad5-1fde-4f8d-871e-f1498a419887")]
        public readonly InputSlot<System.Numerics.Vector4> Background = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "4ba0a4d2-44c8-4c8e-95d1-7b1fbda6975b")]
        public readonly InputSlot<System.Numerics.Vector2> Center = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "00ffc593-d62e-4f40-8957-906b444b8f3b")]
        public readonly InputSlot<float> Divisions = new InputSlot<float>();

        [Input(Guid = "e635d5cd-8250-4722-a11a-ffe27a922f3e")]
        public readonly InputSlot<float> LineThickness = new InputSlot<float>();

        [Input(Guid = "dfdeeb3d-ce18-48a6-9f18-a0aea28b2fff")]
        public readonly InputSlot<float> Width = new InputSlot<float>();

        [Input(Guid = "0c227ab3-3394-40fb-9274-d857a2293d35")]
        public readonly InputSlot<float> Rotation = new InputSlot<float>();

        [Input(Guid = "553b006b-6cab-4a40-915e-dc5038ff5032")]
        public readonly InputSlot<float> PingPong = new InputSlot<float>();

        [Input(Guid = "995307c3-f7c7-41c5-a1a8-49fe32a54716")]
        public readonly InputSlot<float> SmoothGradient = new InputSlot<float>();

        [Input(Guid = "03afbb91-a64f-48bc-a306-b378a068aca8")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "4ed054c5-2a48-4717-9984-a191c26db163")]
        public readonly InputSlot<float> Bias = new InputSlot<float>();

        [Input(Guid = "f238894d-4b34-47fe-9fe4-e28b630b5e99")]
        public readonly InputSlot<System.Numerics.Vector4> LineColor = new InputSlot<System.Numerics.Vector4>();
    }
}

