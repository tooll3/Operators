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

        [Input(Guid = "164dcb7e-25ec-4ca9-a71b-dfd13ec8dc10")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();

        [Input(Guid = "53a2c70b-2523-4f9f-abf8-2e5b16ddfe00")]
        public readonly MultiInputSlot<SharpDX.Direct3D11.ShaderResourceView> ShaderResources = new MultiInputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "39261664-c9e9-40ca-85d0-726dc8801bf2")]
        public readonly InputSlot<T3.Core.DataTypes.ParticleSystem> ParticleSystem = new InputSlot<T3.Core.DataTypes.ParticleSystem>();

        [Input(Guid = "9cbd5aed-e175-4001-aadc-19817cd25c33")]
        public readonly InputSlot<int> FilterEmitter = new InputSlot<int>();
    }
}