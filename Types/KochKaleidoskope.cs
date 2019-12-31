using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class KochKaleidoskope : Instance<KochKaleidoskope>
    {
        [Output(Guid = "15711a30-7d0c-40f4-be68-e2e75c5b4dc5")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "362bcc93-74a0-40fb-a19f-bff8b7246c34")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "79c84135-5390-455b-bf60-53d4002d0606")]
        public readonly InputSlot<float> Scale = new InputSlot<float>();

        [Input(Guid = "912cfd4d-5bef-4fdc-abfc-2f5bf8239875")]
        public readonly InputSlot<System.Numerics.Vector2> Center = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "0b6c67e6-338e-4bb3-bcee-5cc9e2045824")]
        public readonly InputSlot<System.Numerics.Vector2> Offset = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "a1b3ae23-cb29-4e3e-93b6-fd4f1efadbf1")]
        public readonly InputSlot<float> Angle = new InputSlot<float>();

        [Input(Guid = "4b1cea23-5471-4af2-9bed-86ec1e0f3417")]
        public readonly InputSlot<float> AngleOffset = new InputSlot<float>();

        [Input(Guid = "c355fa5c-c1f5-4e80-b1b1-d2d233dea3fc")]
        public readonly InputSlot<int> Steps = new InputSlot<int>();

        [Input(Guid = "a21ed2c3-f7db-4ba7-a125-c2eca3c67c68")]
        public readonly InputSlot<float> Fade = new InputSlot<float>();
    }
}

