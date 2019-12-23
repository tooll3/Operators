using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class _multiImageFxSetup : Instance<_multiImageFxSetup>
    {
        [Output(Guid = "b6bd9c40-1695-46d0-925e-dbaa7882f0ff")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();


    }
}

