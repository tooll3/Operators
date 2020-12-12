using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2b6981f8_f66c_4132_9f37_6536d477ed65
{
    public class CASim : Instance<CASim>
    {
        [Output(Guid = "b55532fe-9582-46cf-b56e-d699b5ecd9d0")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Input(Guid = "419b5101-26ff-4c56-9e9e-09258c5870b2")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture2d = new InputSlot<SharpDX.Direct3D11.Texture2D>();

    }
}