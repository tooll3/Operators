using SharpDX.Direct3D11;
using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_112ea923_a349_412f_8dd3_4d9d9fc42dc6
{
    public class AnalyzeSound : Instance<AnalyzeSound>
    {

        [Input(Guid = "cc3b0ab3-7379-4197-9a84-dc1b368790c5")]
        public readonly InputSlot<float> BeatThreshold = new InputSlot<float>();

        [Input(Guid = "9aff8ca6-d2f5-4bb5-9c38-af798061596c")]
        public readonly InputSlot<float> HihatThreshold = new InputSlot<float>();

        [Input(Guid = "96c60e29-342c-4c20-8e72-5e205e03e305")]
        public readonly InputSlot<float> DetectionSmoothing = new InputSlot<float>();

        [Input(Guid = "1d92b4fa-a8e9-4c1e-891a-65bf7632bdcf")]
        public readonly InputSlot<float> HiRange = new InputSlot<float>();

        [Input(Guid = "89e105e1-a333-465a-b8a5-7b0ee645acbf")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Output(Guid = "70ab1e5d-7947-400a-851b-b1d3d61fdccf")]
        public readonly Slot<float> BeatCount = new Slot<float>();

        [Output(Guid = "01a62ccf-9383-43c8-b4a4-f06e94f33b81")]
        public readonly Slot<float> BeatLevel = new Slot<float>();

        [Output(Guid = "48ee0045-82cb-40b1-8b14-a4a1f495cf49")]
        public readonly Slot<float> HihatCount = new Slot<float>();

        [Output(Guid = "3a98fc87-fb8f-482a-a16d-06de9c0aa691")]
        public readonly Slot<float> HihatLevel = new Slot<float>();

        [Output(Guid = "7d5b7192-f81c-4186-a0e9-864b033caf15")]
        public readonly Slot<float> MovingSum = new Slot<float>();

        [Output(Guid = "6ba78564-8315-49ef-9d47-eb96e0a52fd5")]
        public readonly Slot<T3.Core.Command> Detection = new Slot<T3.Core.Command>();

        [Output(Guid = "1c9c6951-c300-4ada-ae34-783b30f31180")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


    }
}

