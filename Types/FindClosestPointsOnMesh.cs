using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_58afd953_d3fd_44a9_b54b_ccb287edc40c
{
    public class FindClosestPointsOnMesh : Instance<FindClosestPointsOnMesh>
    {

        [Output(Guid = "fdf76150-0448-470b-bf31-c3844f7b84f3")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> Output = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "b9b7bda8-969d-413a-9446-b72a4c5864bb")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Points = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "af7e882f-7aee-4016-95c4-24c88bcc3bfd")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "1626206c-e596-4b59-805b-9be63a8e8b11")]
        public readonly InputSlot<float> Frequency = new InputSlot<float>();

        [Input(Guid = "98136f19-e28e-45be-9e23-e0826fa28e86")]
        public readonly InputSlot<float> Phase = new InputSlot<float>();

        [Input(Guid = "fe7f79bc-8f1b-420f-bc29-14526888191e")]
        public readonly InputSlot<float> Variation = new InputSlot<float>();

        [Input(Guid = "f132e8ab-1d88-487c-9d8f-838f0f9c26ca")]
        public readonly InputSlot<System.Numerics.Vector3> AmountDistribution = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "ce777441-a1e8-4163-bc5f-b70fc6eabda1")]
        public readonly InputSlot<float> RotationLookupDistance = new InputSlot<float>();

        [Input(Guid = "9b26d4d2-1e12-4c7a-9fbb-1aadb43a0454")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Vertices = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "d6ddebd9-3762-4c0e-969e-1a7277d9b272")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Indices = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "603501a2-5581-47ca-a9e1-ab8e09fda1d8")]
        public readonly InputSlot<T3.Core.DataTypes.MeshBuffers> Mesh = new InputSlot<T3.Core.DataTypes.MeshBuffers>();
    }
}

