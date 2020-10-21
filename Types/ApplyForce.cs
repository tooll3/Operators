using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_9c378944_9a57_4ae4_a88e_36c07244bcf7
{
    public class ApplyForce : Instance<ApplyForce>
    {

        [Output(Guid = "d41c5cd6-1902-4fb9-9639-6513906cef79")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> OutBuffer = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "2c31a936-3b5a-4c85-ad9d-7a575453bb0d")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> GPoints = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "4ae50d50-edfb-450d-a334-3afaf69a960b")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "b36d86c6-015f-42f9-a10f-0f3b41da97e8")]
        public readonly InputSlot<float> Frequency = new InputSlot<float>();

        [Input(Guid = "9392f363-99b3-43b8-8dac-6049590f7c21")]
        public readonly InputSlot<float> Phase = new InputSlot<float>();

        [Input(Guid = "40378b36-ab0f-4792-be80-d2354496f0a6")]
        public readonly InputSlot<float> Variation = new InputSlot<float>();

        [Input(Guid = "395d27fa-9969-4879-8804-d9a72b6703f9")]
        public readonly InputSlot<System.Numerics.Vector3> AmountDistribution = new InputSlot<System.Numerics.Vector3>();
    }
}

