using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_51130e6c_53b2_4294_a3cf_b0fa15584acf
{
    public class PointCloudEmitter : Instance<PointCloudEmitter>
    {
        [Output(Guid = "5a3b555d-630d-428f-8e6a-c0f70514f85b")]
        public readonly Slot<T3.Core.Command> Command = new Slot<T3.Core.Command>();

        [Input(Guid = "9be529cc-cc76-4b9c-9796-4b9aa70e203f")]
        public readonly InputSlot<T3.Core.DataTypes.ParticleSystem> ParticleSystem = new InputSlot<T3.Core.DataTypes.ParticleSystem>();

        [Input(Guid = "9f258713-3fd8-4eea-be2e-d8db5f2ef50f")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> Data = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "ee425eea-ca35-44ba-abfb-989ef2b6eba3")]
        public readonly InputSlot<int> EmitterId = new InputSlot<int>();

        [Input(Guid = "05b0e153-b188-4ca7-8155-96bf576e144d")]
        public readonly InputSlot<string> AddParticleShader = new InputSlot<string>();

        [Input(Guid = "250a2442-ec83-42dc-a778-9529ab0a0dfd")]
        public readonly InputSlot<int> EmitRate = new InputSlot<int>();

        [Input(Guid = "471e3836-cdb5-44a1-a16c-94090dc970b9")]
        public readonly InputSlot<bool> Emit = new InputSlot<bool>();
    }
}
