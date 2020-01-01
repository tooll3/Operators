using SharpDX.Direct3D11;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class FloorPlanFractal : Instance<FloorPlanFractal>
    {
        [Output(Guid = "4690ba22-136d-4231-bfcd-95c8ad078848")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();


    }
}

