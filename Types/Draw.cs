using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Draw : Instance<Draw>
    {
        [Output(Guid = "49B28DC3-FCD1-4067-BC83-E1CC848AE55C")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Draw()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager._device;
            var deviceContext = device.ImmediateContext;
            deviceContext.Draw(VertexCount.GetValue(context), VertexStartLocation.GetValue(context));
        }

        [Input(Guid = "8716B11A-EF71-437E-9930-BB747DA818A7")]
        public readonly InputSlot<int> VertexCount = new InputSlot<int>();
        [Input(Guid = "B381B3ED-F043-4001-9BBC-3E3915F38235")]
        public readonly InputSlot<int> VertexStartLocation = new InputSlot<int>();
    }
}