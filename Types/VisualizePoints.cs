using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_37bdbafc_d14c_4b81_91c3_8f63c3b63812
{
    public class VisualizePoints : Instance<VisualizePoints>
    {
        [Output(Guid = "b0294b73-58a9-4d79-b3e2-caaed304109d")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "54fc4cd7-dfc3-4690-9fd1-2b555f7656d4")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> Points = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "621bf2cf-8d49-4b5f-88b9-4460045e8914")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "9c97232c-4a19-4aa1-b671-c4be18676d8c")]
        public readonly InputSlot<float> Width = new InputSlot<float>();

        [Input(Guid = "a9681c53-bf00-4a18-8b7e-f5dad9d0d38d")]
        public readonly InputSlot<bool> IsEnabled = new InputSlot<bool>();

        [Input(Guid = "C85649DF-A235-49D6-A964-C69B299FB4B5")]
        public readonly InputSlot<EvaluationContext.GizmoVisibility> Visibility = new InputSlot<EvaluationContext.GizmoVisibility>();

    }
}

