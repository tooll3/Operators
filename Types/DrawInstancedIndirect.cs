using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class DrawInstancedIndirect: Instance<DrawInstancedIndirect>
    {
        [Output(Guid = "3A8880AF-BBBF-4560-B0C7-6E643A20FC20")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public DrawInstancedIndirect()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            Buffer buffer = Buffer.GetValue(context);
            if (buffer == null)
                return;
            
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager._device;
            var deviceContext = device.ImmediateContext;
            deviceContext.DrawInstancedIndirect(buffer, AlignedByteOffsetForArgs.GetValue(context));
        }

        [Input(Guid = "6C87816C-DA1D-4429-A1F6-61233AA3D7B1")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> Buffer = new InputSlot<Buffer>();
        [Input(Guid = "BC874135-45F2-45E2-8005-244B9123ED20")]
        public readonly InputSlot<int> AlignedByteOffsetForArgs = new InputSlot<int>();
    }
}