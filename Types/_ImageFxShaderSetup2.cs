using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class _ImageFxShaderSetup2 : Instance<_ImageFxShaderSetup2>
    {
        [Output(Guid = "36e01dc9-0680-4af5-9329-0ac6a5f78f8c")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();

        
        [Input(Guid = "16dce9bc-02d9-48f8-b07e-94a9af48d27b")]
        public readonly InputSlot<string> Source = new InputSlot<string>();


        [Input(Guid = "36abde68-0d79-4fb6-b8ce-ac8a27b66850")]
        public readonly InputSlot<Texture2D> Texture = new InputSlot<Texture2D>();
        
        [Input(Guid = "8e9b8826-5870-4db6-93c2-9fb469cfeec9")]
        public readonly MultiInputSlot<float> Params = new MultiInputSlot<float>();

    }
}

