using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class MondrianScope2 : Instance<MondrianScope2>
    {
        [Output(Guid = "87cd296e-8d8f-4c12-9811-801caed579c3")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

