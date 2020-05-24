using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_26a34630_ad46_4bcc_8ff8_ed37fe021f6c
{
    public class OBSOLETE_Displace : Instance<OBSOLETE_Displace>
    {
        [Output(Guid = "0fd9e62e-e9f8-441d-95e4-84a77e368d84")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        
        [Input(Guid = "08cc84b0-9ede-49d1-bf3f-52b229b7ec55")]
        public readonly InputSlot<Texture2D> Image = new InputSlot<Texture2D>();

        [Input(Guid = "A9EF5673-9A48-459D-B08C-B03F3C62CC6F")]
        public readonly InputSlot<Texture2D> DisplaceMap = new InputSlot<Texture2D>();

        
        [Input(Guid = "fbf48b39-135b-4e5d-bd90-5671d4070d52")]
        public readonly InputSlot<float> SampleRadius = new InputSlot<float>();

        [Input(Guid = "3aefdcee-7d34-41eb-b999-9bae7b792941")]
        public readonly InputSlot<float> DisplaceAmount = new InputSlot<float>();

        [Input(Guid = "ca123b90-e609-4df4-a866-421c272c83fd")]
        public readonly InputSlot<float> DisplaceOffset = new InputSlot<float>();
        
        [Input(Guid = "E13050AB-FB47-4EE1-BDED-76AACEBC203F")]
        public readonly InputSlot<float> SampleCount = new InputSlot<float>();

        [Input(Guid = "E4DF2330-D25F-4BD8-8BA2-104408316985")]
        public readonly InputSlot<float> ShiftX = new InputSlot<float>();
        
        [Input(Guid = "E859E00F-1AFA-4925-8D7B-DD376F74A546")]
        public readonly InputSlot<float> ShiftY = new InputSlot<float>();

        [Input(Guid = "EEE699C6-51E9-45E9-A1FB-0A7AEA68130A")]
        public readonly InputSlot<float> Angle = new InputSlot<float>();
    }
}