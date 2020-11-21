using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_cbbb011c_f2bc_460e_86d0_48e49ed377fd
{
    public class SortPixelGlitch : Instance<SortPixelGlitch>
    {
        [Output(Guid = "5d93420b-af9c-45bb-8f48-0318b2718d88")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Input(Guid = "c1be39f5-9516-4a25-a57d-20aa56d68fa7")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture2d = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "0a861871-a6d5-41f1-932d-639ca1afcaf7")]
        public readonly InputSlot<float> MaxSteps = new InputSlot<float>();

        [Input(Guid = "f3d77ff3-bd0c-4d36-93c3-5bb6cbc5397d")]
        public readonly InputSlot<float> Threshold = new InputSlot<float>();

        [Input(Guid = "f2eaa551-64f8-475f-b80a-a2b659393157")]
        public readonly InputSlot<float> Extend = new InputSlot<float>();

        [Input(Guid = "cf4d392f-426a-4451-b752-25009e843a63")]
        public readonly InputSlot<float> GradientBias = new InputSlot<float>();

        [Input(Guid = "0d589063-aadf-47e5-8eb0-1c9beba104d0")]
        public readonly InputSlot<System.Numerics.Vector4> BackgroundColor = new InputSlot<System.Numerics.Vector4>();

    }
}