using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Helper;

namespace T3.Operators.Types
{
    public class ImageQuadEmitter : Instance<ImageQuadEmitter>
    {
        [Output(Guid = "23c5d230-4f31-44f8-9b65-5c8cf111bd45")]
        public readonly Slot<Command> Command = new Slot<Command>();

        [Input(Guid = "3fc23e8b-95bc-467d-8e60-3794b9135c34")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();
        [Input(Guid = "49a41a2a-39a4-46fd-ab70-5c8442a6fd61")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();

        [Input(Guid = "d133b57d-4e2c-432a-8172-599e7f70e79e")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();

        [Input(Guid = "892C9FC8-817D-49D5-97D2-CB290C609AEB")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "241961F6-27F2-4D1D-85EE-51A819C3AE21")]
        public readonly InputSlot<float> Mass = new InputSlot<float>();

        [Input(Guid = "6EF0891C-B6CD-4EEC-A570-11C38E889AD5")]
        public readonly InputSlot<float> LifeTime = new InputSlot<float>();

        [Input(Guid = "59213885-EBBB-41CF-A6BC-B0A4B657D22B")]
        public readonly InputSlot<float> EmitPosY = new InputSlot<float>();

        [Input(Guid = "F5FA3F1E-33C3-4DF3-96D2-981F166772AF")]
        public readonly InputSlot<float> EmitPosYScatter = new InputSlot<float>();

    }
}