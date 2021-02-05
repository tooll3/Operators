using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_13ff9adb_2634_4129_8bb4_4fb764d38be6
{
    public class SmoothPoints : Instance<SmoothPoints>
    {

        [Output(Guid = "28cba376-7037-4d8c-bc4b-a8c747687f03")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> Output = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "78f5d842-960f-4885-a65b-defd04871091")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Points = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "3d50d3c5-07e6-4246-8740-fcdc62173e1d")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "71e20371-578b-4a7c-93fc-feb8f583b600")]
        public readonly InputSlot<float> Subdivisions = new InputSlot<float>();
    }
}

