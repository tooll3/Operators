using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types.Id_d8c5330f_59b5_4907_b845_a02def3042fa
{
    public class Layer2d : Instance<Layer2d>
    {
        [Output(Guid = "e4a8d926-7abd-4d2a-82a1-b7d140cb457f")]
        public readonly Slot<Command> Output = new Slot<Command>();


        [Input(Guid = "2a95ac54-5ef7-4d3c-a90b-ecd5b422bddc")]
        public readonly InputSlot<Texture2D> Texture = new InputSlot<Texture2D>();

        [Input(Guid = "ed4f8c30-7b71-4649-97e6-710a718039b0")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

    }
}

