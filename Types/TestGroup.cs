using System;
using System;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class TestGroup : Instance<TestGroup>
    {

        [Input(Guid = "7a74bd78-2861-4e8f-8f86-b4b330ad1536")]
        public readonly InputSlot<float> Input1 = new InputSlot<float>();

        [Input(Guid = "8c3d9cb6-2002-423b-8455-96e1c3242bc0")]
        public readonly InputSlot<int> Numerator = new InputSlot<int>();

    }
}
