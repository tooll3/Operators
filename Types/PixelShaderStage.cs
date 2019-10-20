using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class PixelShaderStage : Instance<PixelShaderStage>
    {
        [Output(Guid = "76E7AD5D-A31D-4B1F-9C42-B63C5161117C")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public PixelShaderStage()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always; // always render atm
        }

        private void UpdateMultiInput<T>(MultiInputSlot<T> input, ref T[] resources, EvaluationContext context)
        {
            if (input.DirtyFlag.IsDirty)
            {
                var connectedInputs = input.GetCollectedTypedInputs();
                if (connectedInputs.Count != resources.Length)
                {
                    resources = new T[connectedInputs.Count];
                }

                for (int i = 0; i < connectedInputs.Count; i++)
                {
                    resources[i] = connectedInputs[i].GetValue(context);
                }

                input.DirtyFlag.Clear();
            }
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager._device;
            var deviceContext = device.ImmediateContext;
            var psStage = deviceContext.PixelShader;

            var ps = PixelShader.GetValue(context);

            UpdateMultiInput(ConstantBuffers, ref _constantBuffers, context);
//            UpdateMultiInput(ShaderResources, ref _shaderResourceViews, context);
//            UpdateMultiInput(SamplerStates, ref _samplerStates, context);
//            UpdateMultiInput(Uavs, ref _uavs, context);

            if (ps == null)
                return;

            psStage.Set(ps);
            psStage.SetConstantBuffers(0, _constantBuffers.Length, _constantBuffers);
            psStage.SetShaderResources(0, _shaderResourceViews.Length, _shaderResourceViews);
//            psStage.SetSamplers(0, _samplerStates);
//            psStage.SetUnorderedAccessViews(0, _uavs);

            // unbind resources
//            for (int i = 0; i < _uavs.Length; i++)
//                psStage.SetUnorderedAccessView(i, null);
//            for (int i = 0; i < _samplerStates.Length; i++)
//                psStage.SetSampler(i, null);
//            for (int i = 0; i < _shaderResourceViews.Length; i++)
//                psStage.SetShaderResource(i, null);
//            for (int i = 0; i < _constantBuffers.Length; i++)
//                psStage.SetConstantBuffer(i, null);
        }

        private Buffer[] _constantBuffers = new Buffer[0];
        private ShaderResourceView[] _shaderResourceViews = new ShaderResourceView[0];
        private SamplerState[] _samplerStates = new SamplerState[0];

        [Input(Guid = "1B9BE6EB-96C8-4B1C-B854-99B64EAF5618")]
        public readonly InputSlot<SharpDX.Direct3D11.PixelShader> PixelShader = new InputSlot<SharpDX.Direct3D11.PixelShader>();
        [Input(Guid = "BE02A84B-A666-4119-BB6E-FEE1A3DF0981")]
        public readonly MultiInputSlot<Buffer> ConstantBuffers = new MultiInputSlot<Buffer>();
        [Input(Guid = "50052906-4691-4A84-A69D-A109044B5300")]
        public readonly MultiInputSlot<ShaderResourceView> ShaderResources = new MultiInputSlot<ShaderResourceView>();
        [Input(Guid = "C4E91BC6-1691-4EB4-AED5-DD4CAE528149")]
        public readonly MultiInputSlot<SamplerState> SamplerStates = new MultiInputSlot<SamplerState>();
    }
}