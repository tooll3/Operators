using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_18251874_5d5a_4384_8dcd_fcf297e54886
{
    public class PickPoints : Instance<PickPoints>
    {

        [Output(Guid = "bb886ff1-31a9-47aa-a39a-fa60ebb6c2d6")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> Output = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "3b193782-2a56-4031-a0c6-9ebb576e66a5")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Points = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "ceff3a76-5ec6-4be6-afc8-aed1722aa00b")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "a4b64b12-8a87-437f-8201-ede37b487142")]
        public readonly InputSlot<float> Frequency = new InputSlot<float>();

        [Input(Guid = "7ff28497-9eb8-494b-8c1e-6e1ef409b2ca")]
        public readonly InputSlot<float> Phase = new InputSlot<float>();

        [Input(Guid = "f36e5479-09a7-4a98-81c6-5764c933036e")]
        public readonly InputSlot<float> Variation = new InputSlot<float>();

        [Input(Guid = "974b558b-43b6-427d-ae4f-262bfa301c74")]
        public readonly InputSlot<System.Numerics.Vector3> AmountDistribution = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "ff51ccf9-0749-48be-88ab-51604024cc55")]
        public readonly InputSlot<float> RotationLookupDistance = new InputSlot<float>();
    }
}

