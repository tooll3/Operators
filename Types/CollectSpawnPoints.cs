using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_dc3d1571_ad9f_46aa_bed9_df2f4e1c7040
{
    public class CollectSpawnPoints : Instance<CollectSpawnPoints>
    {

        [Output(Guid = "fd2f84af-0925-418e-b3fa-edec6fa19df3")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "088f9a81-7170-4f9d-bbfa-f08b0bf32317")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "18903940-ff20-4b64-a4f0-6078977edd7a")]
        public readonly InputSlot<int> BufferSize = new InputSlot<int>();

        [Input(Guid = "5525b00a-eea5-46ed-b4b4-cbcadcee3820")]
        public readonly InputSlot<bool> CollectPoints = new InputSlot<bool>();

        [Input(Guid = "e801acdb-7750-4cd4-9366-ea2d17dbb649")]
        public readonly InputSlot<int> Mode = new InputSlot<int>();
    }
}

