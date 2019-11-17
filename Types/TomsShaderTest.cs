using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class TomsShaderTest : Instance<TomsShaderTest>
    {
        [Output(Guid = "AF7084FB-2B83-4554-A228-46763AA054EC")]
        public readonly Slot<Texture2D> Texture = new Slot<Texture2D>();
    }
}

