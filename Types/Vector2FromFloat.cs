using System;
using SharpDX;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Vector2FromFloat : Instance<Vector2FromFloat>
    {
        [Output(Guid = "6276597C-580F-4AA4-B066-2735C415FD7C")]
        public readonly Slot<System.Numerics.Vector2> Result = new Slot<System.Numerics.Vector2>();

        
        public Vector2FromFloat()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Result.Value = new System.Numerics.Vector2(X.GetValue(context), Y.GetValue(context));
        }
        
        [Input(Guid = "6b9d0106-78f9-4507-a0f6-234c5dfb0f85")]
        public readonly InputSlot<float> X = new InputSlot<float>();

        [Input(Guid = "2d9c040d-5244-40ac-8090-d8d57323487b")]
        public readonly InputSlot<float> Y = new InputSlot<float>();
        
    }
}
