using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c6fce5e7_391e_4d08_bdc2_4c4960a2296e
{
    public class ParticleUpdate : Instance<ParticleUpdate>
    {
        [Output(Guid = "A2FC8C88-2DD2-4099-A76E-B252032F41CC")]
        public readonly Slot<Command> Command = new Slot<Command>();

        [Input(Guid = "164DCB7E-25EC-4CA9-A71B-DFD13EC8DC10")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();
        [Input(Guid = "39261664-C9E9-40CA-85D0-726DC8801BF2")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();
        [Input(Guid = "53A2C70B-2523-4F9F-ABF8-2E5B16DDFE00")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
    }
}