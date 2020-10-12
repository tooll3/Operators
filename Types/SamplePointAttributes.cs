using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b3de7a93_e921_4e43_8a56_6c84b2d18b74
{
    public class SamplePointAttributes : Instance<SamplePointAttributes>
    {

        [Output(Guid = "c0d516e4-f39f-41c0-9e63-38e5728b3840")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> OutBuffer2 = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "d156d55f-8847-4dea-a33c-525b0a43992f")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> SourcePoints = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "2b5ee35d-bb00-4dd1-abdc-c4584c7ce7c5", MappedType = typeof(Attributes))]
        public readonly InputSlot<int> Brightness = new InputSlot<int>();

        [Input(Guid = "ce9c0841-a1f3-4f5d-9cd8-636390112ece")]
        public readonly InputSlot<float> BrightnessFactor = new InputSlot<float>();

        [Input(Guid = "62e68350-739a-4220-bb84-70b33ea1baf0")]
        public readonly InputSlot<float> BrightnessOffset = new InputSlot<float>();

        [Input(Guid = "f5f424a1-42a4-4429-8d99-47a7c6176400", MappedType = typeof(Attributes))]
        public readonly InputSlot<int> Red = new InputSlot<int>();

        [Input(Guid = "1531e949-fe17-4718-9ac6-4dc3884c23fc")]
        public readonly InputSlot<float> RedFactor = new InputSlot<float>();

        [Input(Guid = "022f5d7d-b9fc-4c4f-8b7e-84aaedbfdd29")]
        public readonly InputSlot<float> RedOffset = new InputSlot<float>();

        [Input(Guid = "d87c9071-2636-4136-b488-410591be47c6", MappedType = typeof(Attributes))]
        public readonly InputSlot<int> Green = new InputSlot<int>();

        [Input(Guid = "6b05ba32-f445-4403-aaba-160c7876b03b")]
        public readonly InputSlot<float> GreenFactor = new InputSlot<float>();

        [Input(Guid = "f6b64f46-ce50-43cb-bc83-e1a3822067db")]
        public readonly InputSlot<float> GreenOffset = new InputSlot<float>();

        [Input(Guid = "c631114f-9ebc-4ea6-b6e5-4c999144e36c", MappedType = typeof(Attributes))]
        public readonly InputSlot<int> Blue = new InputSlot<int>();

        [Input(Guid = "d4a0385e-1dfb-4af0-bf52-5fab61f713bb")]
        public readonly InputSlot<float> BlueFactor = new InputSlot<float>();

        [Input(Guid = "b9cff4dd-52cd-4a36-ab17-b04794402d94")]
        public readonly InputSlot<float> BlueOffset = new InputSlot<float>();

        [Input(Guid = "d1f3b362-7ed4-4833-99e9-0fdc46ca2319")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "a82bc040-a398-41ed-93e1-74309d44a663")]
        public readonly InputSlot<System.Numerics.Vector3> Center = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "7e86bf8f-1d9d-4212-bf9f-987a03b55565")]
        public readonly InputSlot<System.Numerics.Vector2> TextureScale = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "9c53bca4-57fc-495f-ba07-02278c023680")]
        public readonly InputSlot<System.Numerics.Vector3> TextureRotate = new InputSlot<System.Numerics.Vector3>();


        private enum Attributes
        {
            NotUsed,
            For_X,
            For_Y,
            For_Z,
            For_W,
            Rotate_X,
            Rotate_Y,
            Rotate_Z,
        }
    }
}

