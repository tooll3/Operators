using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Helper;

namespace T3.Operators.Types
{
    public class CurlNoise : Instance<CurlNoise>
    {
        [Output(Guid = "a507e4cd-1b47-4cf5-924c-29ec6374f34e")]
        public readonly Slot<Command> Command = new Slot<Command>();

        [Input(Guid = "d8f26ecf-f7eb-44c9-9911-bc177c771c0e")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();
        [Input(Guid = "18d7ff8e-4118-4e65-91eb-917d085f5384")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();
        [Input(Guid = "ffe9dbb2-97ef-46e9-b365-490341624daf")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
    }
}