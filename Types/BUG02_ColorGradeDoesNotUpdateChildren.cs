using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class BUG02_ColorGradeDoesNotUpdateChildren : Instance<BUG02_ColorGradeDoesNotUpdateChildren>
    {
        [Output(Guid = "7e044d64-f6ab-4a23-aaa9-c1d61a8c59fe")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();


    }
}

