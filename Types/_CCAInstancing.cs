using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_7f40b06a_d5f9_490a_9acc_24eff78e3b40
{
    public class _CCAInstancing : Instance<_CCAInstancing>
    {
        [Output(Guid = "f9144007-9d95-459d-8a0e-62489fb76a91")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Input(Guid = "cd6ce054-b3ed-443b-85e2-8e35c578afc9")]
        public readonly InputSlot<float> Threshold = new InputSlot<float>();

        [Input(Guid = "ed0a2cdb-df5e-42a1-93c6-28104c6366fd")]
        public readonly InputSlot<float> MaxSteps = new InputSlot<float>();

        [Input(Guid = "ab4b4b7a-e15b-49d5-8287-ea734c484629")]
        public readonly InputSlot<float> Range = new InputSlot<float>();

        [Input(Guid = "d94db85b-5fb4-47a6-821a-7ff3cc92dca6")]
        public readonly InputSlot<float> RandomAmount = new InputSlot<float>();

        [Input(Guid = "06c20555-c606-4dcd-9050-04c85a0b6098")]
        public readonly InputSlot<bool> AddNoise2 = new InputSlot<bool>();

        [Input(Guid = "523436c5-397b-401a-9060-f3c6fd070f44")]
        public readonly InputSlot<float> R_xThreshold = new InputSlot<float>();

        [Input(Guid = "aa684bed-dac0-4358-9c2b-e1d37dda04d7")]
        public readonly InputSlot<float> G_xStates = new InputSlot<float>();

        [Input(Guid = "81055dac-d6f0-4b80-90dd-94b517e55fe8")]
        public readonly InputSlot<bool> UseMooreRegion = new InputSlot<bool>();

        [Input(Guid = "8f4be08a-9d47-4628-b214-31c7b5f59b3f")]
        public readonly InputSlot<int> PointCount = new InputSlot<int>();

        [Input(Guid = "39a41a0b-51d9-4f99-a9a6-dab6a9adb9e6")]
        public readonly InputSlot<int> Resolution = new InputSlot<int>();

        [Input(Guid = "128c060f-2da7-41e4-bccd-fef1f27e367c")]
        public readonly InputSlot<float> PointThreshold = new InputSlot<float>();

        [Input(Guid = "d010338b-9685-4256-bbc0-afeb16cb4363")]
        public readonly InputSlot<SharpDX.Size2> PointGridRez = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "86602a9e-d676-4df5-83dc-ecea45b35673")]
        public readonly InputSlot<float> PointSize = new InputSlot<float>();

    }
}