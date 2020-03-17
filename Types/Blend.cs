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

        [Input(Guid = "c7c524cf-e31e-4bac-8f77-58bd61b337de")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> ImageB = new InputSlot<SharpDX.Direct3D11.Texture2D>();

    }
}

