using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_dd586355_64b3_4e96_af6d_b4927595dee7
{
    public class AddNoise : Instance<AddNoise>
    {

        [Output(Guid = "9e73ace4-b94c-4bd3-91e5-5bfdac427760")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> OutBuffer2 = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "ab4f41b6-a6c0-44b8-af91-24d1b133b256")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> PointsA = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "5894156a-cc31-4236-908c-de0e5385fd84")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "929db7b2-f19c-4a28-b4c2-187365b99760")]
        public readonly InputSlot<float> Frequency = new InputSlot<float>();

        [Input(Guid = "aaba1602-e7a1-4b48-81d4-9d7b2b3aa8b1")]
        public readonly InputSlot<float> Phase = new InputSlot<float>();

        [Input(Guid = "1dfb45ae-b376-41ea-a1d2-97b170645b50")]
        public readonly InputSlot<float> Variation = new InputSlot<float>();

        [Input(Guid = "c2df1fa3-88e1-4be2-954e-8c44edd9d421")]
        public readonly InputSlot<System.Numerics.Vector3> AmountDistribution = new InputSlot<System.Numerics.Vector3>();
    }
}

