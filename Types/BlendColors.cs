using System.Numerics;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_6b7c541a_ca36_4f21_ac95_89e874820c5a
{
    public class BlendColors : Instance<BlendColors>
    {
        [Output(Guid = "66ce8660-253c-4a0b-8aec-f7a56751a1e4")]
        public readonly Slot<Vector4> Color = new Slot<Vector4>();

        public BlendColors()
        {
            Color.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var a = ColorA.GetValue(context);
            var b = ColorB.GetValue(context);
            var m = Mix.GetValue(context);
            Color.Value = a * (1-m) + b * m;
        }


        [Input(Guid = "EB601C57-2025-4135-8292-223EAEDAF187")]
        public readonly InputSlot<Vector4> ColorA = new InputSlot<Vector4>();
        
        [Input(Guid = "B9E5C6F3-7052-456F-9D1B-C182B4412433")]
        public readonly InputSlot<Vector4> ColorB = new InputSlot<Vector4>();
        
        [Input(Guid = "40803D0E-C37C-4B5D-B64B-FD1EF090A4F7")]
        public readonly InputSlot<float> Mix = new InputSlot<float>(1f);
        
    }
}