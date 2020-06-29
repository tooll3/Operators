using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_fd31d208_12fe_46bf_bfa3_101211f8f497
{
    public class RenderText : Instance<RenderText>
    {
        [Output(Guid = "3f8b20a7-c8b8-45ab-86a1-0efcd927358e")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "f1f1be0e-d5bc-4940-bbc1-88bfa958f0e1")]
        public readonly InputSlot<string> Text = new InputSlot<string>();

        [Input(Guid = "e38128d9-1296-4a24-a23c-67cdbb37a4b2")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> DisplaceTexture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "fb364615-fab2-4d64-8754-ade05f7cbada")]
        public readonly InputSlot<SharpDX.Size2> GridSize = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "ba9c0d8f-dbe3-4c98-9d02-523ca31ab0c4")]
        public readonly InputSlot<System.Numerics.Vector2> CellSize = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "f46dbc18-d5de-4727-80b0-c4376e2fc6b3")]
        public readonly InputSlot<System.Numerics.Vector2> CellPadding = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "93e6b01d-d099-45cc-8606-55fb56d9d487")]
        public readonly InputSlot<System.Numerics.Vector2> TextOffset = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "0e5f05b4-5e8a-4f6d-8cac-03b04649eb67")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "bea346da-c22c-4d85-b4a9-84cf062d8998")]
        public readonly InputSlot<System.Numerics.Vector3> DisplaceAmount = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "1ea17b6a-c678-4335-9fd0-328928c971c4")]
        public readonly InputSlot<bool> WrapText = new InputSlot<bool>();

        [Input(Guid = "a10c0c0d-465a-4aed-94da-bca0daf7d95e")]
        public readonly InputSlot<float> OverrideScale = new InputSlot<float>();

        [Input(Guid = "e5221f1b-cafe-48ee-9bf6-546344181af7")]
        public readonly InputSlot<System.Numerics.Vector4> HighlightColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "540d1804-dc32-4180-8a38-22c5b30e91d8")]
        public readonly InputSlot<string> HighlightChars = new InputSlot<string>();
    }
}

