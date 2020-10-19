using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_780edb20_f83f_494c_ab17_7015e2311250
{
    public class RepeatGPoints : Instance<RepeatGPoints>
    {

        [Output(Guid = "3ac76b2a-7b1c-4762-a3f6-50529cd42fa8")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "47c3c549-78bb-41fd-a88c-58f643870b40")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "a952d91a-a86b-4370-acd9-e17b19025966")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GTargets = new InputSlot<T3.Core.DataTypes.BufferWithViews>();
    }
}

