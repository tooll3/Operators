using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c454abd8_62a8_4413_a463_668013a6a5bd
{
    public class RaymarchFractal : Instance<RaymarchFractal>
    {
        [Output(Guid = "da8c003a-893f-402e-a8c2-d92b41d4ea00")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "f9cc2427-135d-492e-b25d-276715ab82f3")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "a2d5dccc-2bca-48d3-82ca-ad75ef937000")]
        public readonly InputSlot<System.Numerics.Vector4> Fill = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "f9c2ad47-c263-47c2-bd6a-a5fbe664a18e")]
        public readonly InputSlot<System.Numerics.Vector4> Background = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "7216c97d-3ae2-4a63-b7aa-084aee88b4f3")]
        public readonly InputSlot<System.Numerics.Vector2> Size = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "f730628f-d609-4b5b-8c8a-66af306c9a8d")]
        public readonly InputSlot<System.Numerics.Vector2> Position = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "6e028b0b-f89f-45d1-bdf3-9a26550fe93b")]
        public readonly InputSlot<float> Round = new InputSlot<float>();

        [Input(Guid = "3201cb10-0268-4cc4-9c56-6fad54d135f9")]
        public readonly InputSlot<float> Feather = new InputSlot<float>();

        [Input(Guid = "25c5dbb4-8ac7-49f3-ba18-14c747159511")]
        public readonly InputSlot<float> FeatherBias = new InputSlot<float>();

        [Input(Guid = "3522be02-01e2-4ac4-8e73-bf81eeb71cf0")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();
    }
}

