using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class VertexShaderStage : Instance<VertexShaderStage>
    {
        [Output(Guid = "65B394A9-06DC-4D9B-8819-15394EDE2997")]
        public readonly Slot<Command> Output = new Slot<Command>(new Command());

        public VertexShaderStage()
        {
            Output.UpdateAction = Update;
            Output.Value.RestoreAction = Restore;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager._device;
            var deviceContext = device.ImmediateContext;
            var vsStage = deviceContext.VertexShader;

            var vs = VertexShader.GetValue(context);

            if (vs == null)
                return;

            _prevVertexShader = vs;
            vsStage.Set(vs);
        }

        private void Restore(EvaluationContext context)
        {
            var deviceContext = ResourceManager.Instance()._device.ImmediateContext;
            var vsStage = deviceContext.VertexShader;
            vsStage.Set(_prevVertexShader);
        }

        SharpDX.Direct3D11.VertexShader _prevVertexShader;

        [Input(Guid = "B1C236E5-6757-4D77-9911-E3ACD5EA9FE9")]
        public readonly InputSlot<SharpDX.Direct3D11.VertexShader> VertexShader = new InputSlot<SharpDX.Direct3D11.VertexShader>();
        [Input(Guid = "BBA8F6EB-7CFF-435B-AB47-FEBF58DD8FBA")]
        public readonly MultiInputSlot<Buffer> ConstantBuffers = new MultiInputSlot<Buffer>();
        [Input(Guid = "3A0BEA89-BD93-4594-B1B6-3E25689C67E6")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
        [Input(Guid = "2BC7584D-A347-4954-9120-C1841AF76650")]
        public readonly MultiInputSlot<SamplerState> SamplerStates = new MultiInputSlot<SamplerState>();
    }
}