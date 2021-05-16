using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_7246323c_5cd5_4db1_af25_3e96ce385c23
{
    public class DispersePoints : Instance<DispersePoints>
    {

        [Output(Guid = "77b50dfa-f9e4-4c9b-89d0-12b99c014c4c")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "b465e626-e8bf-457e-b192-8f3b72a1d443")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> PointsA_ = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "94219a67-deae-46bc-844b-97a29cc5bea5")]
        public readonly InputSlot<float> Threshold = new InputSlot<float>();

        [Input(Guid = "fa0f6143-24a5-4277-a23a-3ac535f29fed")]
        public readonly InputSlot<float> Dispersion = new InputSlot<float>();
    }
}

