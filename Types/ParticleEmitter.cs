using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Helper;

namespace T3.Operators.Types
{
    public class ParticleEmitter : Instance<ParticleEmitter>
    {
        [Output(Guid = "AA24848D-4EC6-4F1A-9A5A-C64587EE6F75")]
        public readonly Slot<Command> Command = new Slot<Command>();

        [Input(Guid = "B33B6B8B-37CD-475A-B67D-AC672478F439")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();
        [Input(Guid = "7F5E613E-03F0-48A0-9844-54E78868F7E3")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();
        [Input(Guid = "88AD8D03-0281-417F-8830-69CCAD5345AD")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
    }
}