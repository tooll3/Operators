using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class BugMultipleTransforms : Instance<BugMultipleTransforms>
    {
        [Output(Guid = "160bb9ff-3083-42aa-98a4-54884f43b8cb")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();


    }
}

