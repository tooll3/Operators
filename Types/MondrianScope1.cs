using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class MondrianScope1 : Instance<MondrianScope1>
    {
        [Output(Guid = "7205d8cd-1e58-435e-8265-2b028f0773e3")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

