using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class TestLab : Instance<TestLab>
    {
        [Output(Guid = "486eb33f-111d-4d6e-b81e-9f2ca16cb729")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();


    }
}

