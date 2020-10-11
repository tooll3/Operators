using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_3ee8f66d_68df_43c1_b0eb_407259bf7e86
{
    public class GridGPoints : Instance<GridGPoints>
    {

        [Output(Guid = "bb0ef759-9746-4f99-8641-ae6173e789ad")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> OutBuffer2 = new Slot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "72eda38f-fc49-4b1f-b7c0-97e07bee4f7c")]
        public readonly InputSlot<int> CountX = new InputSlot<int>();

        [Input(Guid = "8c46fc72-8960-4247-a5ef-dd38f822f1bb")]
        public readonly InputSlot<int> CountY = new InputSlot<int>();

        [Input(Guid = "6de4f08a-5834-4b9b-93e8-8f93fe32164c")]
        public readonly InputSlot<int> CountZ = new InputSlot<int>();

        [Input(Guid = "37a11e3d-e353-4b0f-a052-356582e235b0")]
        public readonly InputSlot<System.Numerics.Vector3> Size = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "0f053c34-c9ef-46b7-9c73-fff9984a3d5e")]
        public readonly InputSlot<System.Numerics.Vector3> Center = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "70459c2d-8686-4709-9a12-1ea36d1b08d2")]
        public readonly InputSlot<float> W = new InputSlot<float>();
    }
}

