using SharpDX.Direct3D11;
using System;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_fd9bffd3_5c57_462f_8761_85f94c5a629b
{
    public class PickBlendMode : Instance<PickBlendMode>
    {
        [Output(Guid = "6db5a38e-8397-43fb-af1e-eca3fc3cb7d9")]
        public readonly Slot<Command> Output = new Slot<Command>();


        [Input(Guid = "086424c7-9431-4921-bb67-4f8160cf0416")]
        public readonly InputSlot<DepthStencilState> DepthStencilState = new InputSlot<DepthStencilState>();

        [Input(Guid = "30b58444-0485-4116-8b15-7e62fee69eaa", MappedType = typeof(BlendModes))]
        public readonly InputSlot<int> BlendMode = new InputSlot<int>();

        [Input(Guid = "ee1bf2d2-50a2-496e-b728-d94ffbbd9e5d")]
        public readonly InputSlot<bool> EnableDepthTest = new InputSlot<bool>();

        [Input(Guid = "db45e025-74c0-495c-a81c-6fd25845903b")]
        public readonly InputSlot<bool> EnableZWrite = new InputSlot<bool>();

        enum BlendModes
        {
            Normal,
            Additive,
            Multiply,
            Invert,
        }   
        
    }
}

