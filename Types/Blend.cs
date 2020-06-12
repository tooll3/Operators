using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_9f43f769_d32a_4f49_92ac_e0be3ba250cf
{
    public class Blend : Instance<Blend>
    {
        [Output(Guid = "536fae14-b814-498c-a6b4-07775de36991")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> Output = new Slot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "abaa52e9-7d3d-4ae5-89d2-5251f61e5392")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> ImageA = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "6541c1ac-ba84-4a46-a6df-8ab52455c57b")]
        public readonly InputSlot<System.Numerics.Vector4> ColorA = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "c7c524cf-e31e-4bac-8f77-58bd61b337de")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> ImageB = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "70dc133e-800a-4cd0-a159-2cbab4c322cb")]
        public readonly InputSlot<System.Numerics.Vector4> ColorB = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "fc5f1d08-3997-4ba3-ac59-d86e4e501fb0")]
        public readonly InputSlot<int> BlendMode = new InputSlot<int>();

        [Input(Guid = "cad32967-e91b-4bd1-af09-5fdfdeee630e")]
        public readonly InputSlot<int> AlphaMode = new InputSlot<int>();

    }
}

