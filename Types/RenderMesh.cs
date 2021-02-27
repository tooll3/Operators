using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_a3c5471e_079b_4d4b_886a_ec02d6428ff6
{
    public class RenderMesh : Instance<RenderMesh>
    {
        [Output(Guid = "53b3fdca-9d5e-4808-a02f-4aa743cd8456", DirtyFlagTrigger = DirtyFlagTrigger.Always)]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "8c9dee45-d165-48c8-b8dd-b7f47e77fd00")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "e8aa82cd-f628-4d64-8d98-a342662072e1")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "97429e1f-3f30-4789-89a6-8e930e356ee6")]
        public readonly InputSlot<T3.Core.DataTypes.MeshBuffers> Mesh = new InputSlot<T3.Core.DataTypes.MeshBuffers>();
    }
}

