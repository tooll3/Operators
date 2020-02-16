using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Helper;

namespace T3.Operators.Types.Id_36cec77e_fd3b_4ec7_ac93_74ee67fb97a2
{
    public class TestEmitter : Instance<TestEmitter>
    {
        [Output(Guid = "1600bf44-d2d4-42d9-a75d-c94a6466309b")]
        public readonly Slot<Command> Command = new Slot<Command>();

        [Input(Guid = "60661094-3f20-410a-8387-62e23b98b4be")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();
        [Input(Guid = "69566d1b-fb5b-45c8-abe9-f03ed18fa968")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
        [Input(Guid = "0F4F8D27-391E-492F-9075-70A50ED8EE42")]
        public readonly InputSlot<float> Lifetime = new InputSlot<float>();
        [Input(Guid = "16576E3C-A55E-426E-BF1F-95F18F8648EC")]
        public readonly InputSlot<Vector4> Color = new InputSlot<Vector4>();
        [Input(Guid = "CF063078-99D3-421D-80D4-DA83AA89A859")]
        public readonly InputSlot<Vector4> ScatterColor = new InputSlot<Vector4>();
        [Input(Guid = "B7E871B1-49BB-44E1-9A71-203911A42D97")]
        public readonly InputSlot<Vector4> RandomVelocity = new InputSlot<Vector4>();
    }
}