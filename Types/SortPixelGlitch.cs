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

        [Input(Guid = "2d1c82aa-1c1b-4202-99eb-811541b74ab9")]
        public readonly InputSlot<float> Range = new InputSlot<float>();

        [Input(Guid = "6b7ee425-d1a7-47da-bca3-16dce9be5742")]
        public readonly InputSlot<float> RangeExtend = new InputSlot<float>();

        [Input(Guid = "d52690ff-be34-42a9-af59-923f2731f023")]
        public readonly InputSlot<float> TestParam = new InputSlot<float>();

    }
}