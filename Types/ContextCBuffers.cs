using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_d32a5484_880c_41d4_88ea_6ee1a3e61f0b
{
    public class ContextCBuffers : Instance<ContextCBuffers>
    {

        [Output(Guid = "d4171c74-5a90-4fe9-8334-10f9701c284c")]
        public readonly Slot<Buffer> FogParameters = new Slot<Buffer>();

        public ContextCBuffers()
        {
            FogParameters.UpdateAction = Update;

        }

        private void Update(EvaluationContext context)
        {
            FogParameters.Value = context.FogParameters;
        }
    }
}