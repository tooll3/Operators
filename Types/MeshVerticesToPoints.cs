using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2467e1ed_f7fc_4c90_8230_b80ba6b42a2d
{
    public class MeshVerticesToPoints : Instance<MeshVerticesToPoints>
    {

        [Output(Guid = "53089fc7-3f0b-46c4-81e1-04ecbb92efce")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "ec57e613-a7ac-46aa-bda2-9dd858936097")]
        public readonly InputSlot<int> Count = new InputSlot<int>();

        [Input(Guid = "eeda994d-68e3-4f42-a9db-c6dcae84c5f0")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "f888774f-b3a6-4210-bd0e-5c4e74fb9452")]
        public readonly InputSlot<float> RadiusOffset = new InputSlot<float>();

        [Input(Guid = "64c65790-2299-4886-8834-90dac1b286ea")]
        public readonly InputSlot<System.Numerics.Vector3> Center = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "e53daa52-8d2d-4391-9741-53cc034fd2fd")]
        public readonly InputSlot<System.Numerics.Vector3> Offset = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "88b24437-d033-417c-9c6b-c21c15c1bcdc")]
        public readonly InputSlot<float> StartAngle = new InputSlot<float>();

        [Input(Guid = "280e3075-220a-46cd-a4da-b383b47f60f9")]
        public readonly InputSlot<float> Cycles = new InputSlot<float>();

        [Input(Guid = "2868383b-f90d-4a9e-975b-b9978abcd38a")]
        public readonly InputSlot<System.Numerics.Vector3> Axis = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "e5ab7ae6-d8de-4c92-9130-1082e5a56ba1")]
        public readonly InputSlot<float> W = new InputSlot<float>();

        [Input(Guid = "dad852ac-98d7-48e2-8338-15af1b6c1ced")]
        public readonly InputSlot<float> WOffset = new InputSlot<float>();

        [Input(Guid = "e31a8d8c-79fe-4efb-a2ff-5e2cfaff778e")]
        public readonly InputSlot<bool> CloseCircle = new InputSlot<bool>();
    }
}

