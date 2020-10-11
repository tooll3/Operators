using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_780edb20_f83f_494c_ab17_7015e2311250
{
    public class RepeatGPoints : Instance<RepeatGPoints>
    {

        [Output(Guid = "11523a50-3224-42af-9b42-cfc816303868")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> OutBuffer2 = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "a764db5a-fe3c-46f2-8d02-ae902471578e")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> PointsA = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "fe60a38f-a8e5-4800-955b-ab2c6d906ba9")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> Targets = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();
    }
}

