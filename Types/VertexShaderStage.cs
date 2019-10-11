using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class VertexShaderStage : Instance<VertexShaderStage>
    {
        [Output(Guid = "65B394A9-06DC-4D9B-8819-15394EDE2997")]
        public readonly Slot<Scene> Output = new Slot<Scene>();

        public VertexShaderStage()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always; // always render atm
        }

//        private void UpdateMultiInput<T>(MultiInputSlot<T> input, ref T[] resources, EvaluationContext context)
//        {
//            if (input.DirtyFlag.IsDirty)
//            {
//                var connectedConstBuffers = input.GetCollectedTypedInputs();
//                if (connectedConstBuffers.Count != resources.Length)
//                {
//                    resources = new T[connectedConstBuffers.Count];
//                }
//
//                for (int i = 0; i < connectedConstBuffers.Count; i++)
//                {
//                    resources[i] = connectedConstBuffers[i].GetValue(context);
//                }
//
//                input.DirtyFlag.Clear();
//            }
//        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager._device;
            var deviceContext = device.ImmediateContext;
            var vsStage = deviceContext.VertexShader;

            var vs = VertexShader.GetValue(context);

//            UpdateMultiInput(ConstantBuffers, ref _constantBuffers, context);
//            UpdateMultiInput(ShaderResources, ref _shaderResourceViews, context);
//            UpdateMultiInput(SamplerStates, ref _samplerStates, context);
//            UpdateMultiInput(Uavs, ref _uavs, context);

            if (vs == null)
                return;

            vsStage.Set(vs);
            vsStage.SetConstantBuffers(0, _constantBuffers.Length, _constantBuffers);
            vsStage.SetShaderResources(0, _shaderResourceViews.Length, _shaderResourceViews);
//            vsStage.SetSamplers(0, _samplerStates);
//            vsStage.SetUnorderedAccessViews(0, _uavs);

            // unbind resources
//            for (int i = 0; i < _uavs.Length; i++)
//                vsStage.SetUnorderedAccessView(i, null);
//            for (int i = 0; i < _samplerStates.Length; i++)
//                vsStage.SetSampler(i, null);
//            for (int i = 0; i < _shaderResourceViews.Length; i++)
//                vsStage.SetShaderResource(i, null);
//            for (int i = 0; i < _constantBuffers.Length; i++)
//                vsStage.SetConstantBuffer(i, null);
        }

        private Buffer[] _constantBuffers = new Buffer[0];
        private ShaderResourceView[] _shaderResourceViews = new ShaderResourceView[0];
        private SamplerState[] _samplerStates = new SamplerState[0];
        
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