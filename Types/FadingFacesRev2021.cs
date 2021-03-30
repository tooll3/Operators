using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_18d9721b_e170_4f4d_b630_30445aba5e20
{
    public class FadingFacesRev2021 : Instance<FadingFacesRev2021>
    {
        [Output(Guid = "4b6ba325-0505-4f36-9d33-475848f58b3b")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Output(Guid = "30951983-8dff-4c85-8508-79386a5a5879")]
        public readonly Slot<Texture2D> ColorBuffer = new Slot<Texture2D>();


    }
}

