using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_9c67a8c8_839f_4f67_a949_08cb38b9dffd
{
    public class PointLight : Instance<PointLight>
    {
        [Output(Guid = "32b87a4d-bef3-4646-be76-8f8224ebd5c2")]
        public readonly Slot<Command> Output = new Slot<Command>();


        [Input(Guid = "55dc52d8-51a6-497a-9624-b118e0e27c65")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();

        [Input(Guid = "f6d96a01-dc90-49c7-9152-a6a42bb05218")]
        public readonly InputSlot<System.Numerics.Vector3> Position = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "98155900-1bb9-427a-9c4e-0988fec806cd")]
        public readonly InputSlot<float> Intensity = new InputSlot<float>();

        [Input(Guid = "ff3442c5-95c8-4bd6-a492-cb4a9a597ea1")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "81962c9b-2fcd-432a-b2e7-c31b743ecd02")]
        public readonly InputSlot<float> Range = new InputSlot<float>();

    }
}

