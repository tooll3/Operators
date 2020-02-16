using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;

namespace T3.Operators.Types.Id_c9a7e71c_7311_4d1e_8508_cd580d7a0b8d
{
    public class Project : Instance<Project>
    {
        [Output(Guid = "80E2C6C3-1282-4075-B4E3-86BD67795F1C")]
        public readonly Slot<Texture2D> TextureOutput = new Slot<Texture2D>();

        [Output(Guid = "{3A9734E8-7346-4535-BD54-3B5A735CC6B8}")]
        public readonly Slot<string> Output = new Slot<string>("Project Output");


        [Input(Guid = "{9BF87C5F-2DFE-482E-8BE7-18FC4D6072E4}")]
        public readonly InputSlot<float> Input = new InputSlot<float>();

        [Input(Guid = "{C99A7718-12B1-484E-BA96-B8D9EA7F448F}")]
        public readonly MultiInputSlot<string> MultiInput = new MultiInputSlot<string>();

        [Input(Guid = "f693cb54-30e7-4021-a6b3-0465bdc66c93")]
        public readonly InputSlot<float> Test3 = new InputSlot<float>();
    }
}