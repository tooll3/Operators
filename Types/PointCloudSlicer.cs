using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_13d23535_dfa5_458e_93e3_158d83e0188b
{
    public class PointCloudSlicer : Instance<PointCloudSlicer>
    {
        [Output(Guid = "e8993426-0ce0-4ec2-b335-fa28e568db16")]
        public readonly Slot<T3.Core.Command> Command = new Slot<T3.Core.Command>();

        [Output(Guid = "fb114e91-5b5f-4112-a447-8a1e72582672")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> SlicedData = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Output(Guid = "0fbbaf49-f06e-4f50-a955-4c01a7ad3dea")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        [Input(Guid = "cad42998-190e-467a-b882-8b805fc693e3")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> Data = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "989ab883-907f-4685-b4ae-69c517b86831")]
        public readonly InputSlot<System.Numerics.Vector3> PlaneNormal = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "03708185-bb00-477f-861e-1689c3b0aea1")]
        public readonly InputSlot<System.Numerics.Vector3> PlanePos = new InputSlot<System.Numerics.Vector3>();
    }
}

