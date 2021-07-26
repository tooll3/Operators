using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Interfaces;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_1a241222_200b_417d_a8c7_131e3b48cc36
{
    public class PointsOnSphere : Instance<PointsOnSphere>
    {

        [Output(Guid = "c20f4675-6387-45da-b14f-8d0a3af5e672")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        // public RadialGPoints()
        // {
        //     OutBuffer.TransformableOp = this;
        // }
        //
        // System.Numerics.Vector3 ITransformable.Translation { get => Center.Value; set => Center.SetTypedInputValue(value); }
        // System.Numerics.Vector3 ITransformable.Rotation { get => System.Numerics.Vector3.Zero; set { } }
        // System.Numerics.Vector3 ITransformable.Scale { get => System.Numerics.Vector3.One; set { } }
        //
        // public Action<ITransformable, EvaluationContext> TransformCallback { get => OutBuffer.TransformCallback; set => OutBuffer.TransformCallback = value; }
        
        
        [Input(Guid = "0b42b3e6-a6fd-4edc-88b1-d91f9c775023")]
        public readonly InputSlot<int> Count = new InputSlot<int>();

        [Input(Guid = "0bdc6243-3e52-4b1a-b070-731ed27388c6")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "54b10b26-45e7-45e8-8018-1d16f83f0b0f")]
        public readonly InputSlot<float> RadiusOffset = new InputSlot<float>();

        [Input(Guid = "21140fe1-9fb5-4a79-b03a-7deac242fba2")]
        public readonly InputSlot<System.Numerics.Vector3> Center = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "19993a87-c913-47f1-a71b-aae7a2ac64e4")]
        public readonly InputSlot<System.Numerics.Vector3> Offset = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "813df416-a783-433c-9645-921c885c9840")]
        public readonly InputSlot<float> StartAngle = new InputSlot<float>();

        [Input(Guid = "51f8e768-5e7d-49da-aae7-6c843cefa5b8")]
        public readonly InputSlot<float> Cycles = new InputSlot<float>();

        [Input(Guid = "259891c4-f123-4091-8039-cec740d6dd90")]
        public readonly InputSlot<bool> CloseCircle = new InputSlot<bool>();

        [Input(Guid = "b692b3b1-f102-43de-9265-c6d10a8aaa6d")]
        public readonly InputSlot<System.Numerics.Vector3> Axis = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "936eecbf-6cf0-440b-92b6-5a4bba5f793d")]
        public readonly InputSlot<float> W = new InputSlot<float>();

        [Input(Guid = "a422f49c-3774-4cb0-ba44-57a4a67ba317")]
        public readonly InputSlot<float> WOffset = new InputSlot<float>();

        [Input(Guid = "7bb5d49d-19b5-45d0-ac3d-dc46a0f8ebde")]
        public readonly InputSlot<System.Numerics.Vector3> OrientationAxis = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "d50fd8e9-e515-4bfb-bbbc-28094dc45cbb")]
        public readonly InputSlot<float> OrientationAngle = new InputSlot<float>();
    }
}

