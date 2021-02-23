using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c0feef61_8cec_4418_8889_4001cbe1a957
{
    public class _PhysrumAgents2 : Instance<_PhysrumAgents2>
    {
        [Output(Guid = "9dd9d899-c1b2-4c63-b338-b779b2278f86")]
        public readonly Slot<Texture2D> ImgOutput = new Slot<Texture2D>();

        [Input(Guid = "c22b4247-0989-4452-b9ef-6831a80df778")]
        public readonly InputSlot<float> RestoreLayout = new InputSlot<float>();

        [Input(Guid = "d95ac7c1-c282-4776-9acf-3383981f1d6b")]
        public readonly InputSlot<bool> RestoreLayoutEnabled = new InputSlot<bool>();

        [Input(Guid = "bc0f0973-68c1-4f33-801d-9e9db29f7513")]
        public readonly InputSlot<bool> ShowAgents = new InputSlot<bool>();

        [Input(Guid = "cf5837f9-df50-43d2-97db-07595d91828f")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "8b2521cc-5f34-4833-8faf-09aa85848e7e")]
        public readonly InputSlot<int> AgentCount = new InputSlot<int>();

        [Input(Guid = "1aff9d51-48d0-4d36-93f3-be97c8533201")]
        public readonly InputSlot<int> ComputeSteps = new InputSlot<int>();

        [Input(Guid = "a92f43c8-2d17-4664-b9ed-17872ec68498")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> BreedsBuffer = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "c88f4355-fd80-4e66-8dca-5860d1ca2241")]
        public readonly InputSlot<System.Numerics.Vector4> DecayRatio = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "6516ba78-3b9a-475d-88b7-f88337f6686b")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> EffectTexture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "5100f7d8-a5e0-4630-b646-2db20e0cc4be")]
        public readonly InputSlot<SharpDX.Size2> BlockCount = new InputSlot<SharpDX.Size2>();


    }
}

