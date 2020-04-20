using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_557fcca1_5406_4594_9dd2_9122d3b3a1e2
{
    public class AddPointClound : Instance<AddPointClound>
    {
        [Output(Guid = "598a616c-5b49-4d6a-9b65-38879d78cc38")]
        public readonly Slot<T3.Core.Command> Command = new Slot<T3.Core.Command>();

        [Input(Guid = "c138ad9e-5625-4ac1-b6f9-9fff7aba523e")]
        public readonly InputSlot<T3.Core.Operator.Helper.ParticleSystem> ParticleSystem = new InputSlot<T3.Core.Operator.Helper.ParticleSystem>();
    }
}

