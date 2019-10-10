using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class InputAssembler : Instance<InputAssembler>
    {
        [Output(Guid = "18CAE035-C050-4F98-9E5E-B3A6DB70DDA7")]
        public readonly Slot<Scene> Output = new Slot<Scene>();

        public InputAssembler()
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
            var iaStage = deviceContext.InputAssembler;

//            UpdateMultiInput(ConstantBuffers, ref _constantBuffers, context);
//            UpdateMultiInput(ShaderResources, ref _shaderResourceViews, context);
//            UpdateMultiInput(SamplerStates, ref _samplerStates, context);
//            UpdateMultiInput(Uavs, ref _uavs, context);

            iaStage.InputLayout = InputLayout.GetValue(context);
            iaStage.PrimitiveTopology = PrimitiveTopology.GetValue(context);
//            iaStage.SetVertexBuffers();
            iaStage.SetIndexBuffer(IndexBuffer.GetValue(context), Format.R32_UInt,0); // todo: format and offset set input params
             

//            Int3 dispatchCount = Dispatch.GetValue(context);
//            deviceContext.Dispatch(dispatchCount.X, dispatchCount.Y, dispatchCount.Z);

// unbind resources
//            for (int i = 0; i < _uavs.Length; i++)
//                vsStage.SetUnorderedAccessView(i, null);
//            for (int i = 0; i < _samplerStates.Length; i++)
//                vsStage.SetSampler(i, null);
//            for (int i = 0; i < _shaderResourceViews.Length; i++)
//                vsStage.SetShaderResource(i, null);
//            for (int i = 0; i < _constantBuffers.Length; i++)
//                vsStage.SetConstantBuffer(i, null);
            iaStage.SetIndexBuffer(null, Format.R32_UInt, 0);
        }

        private Buffer[] _constantBuffers = new Buffer[0];
        private ShaderResourceView[] _shaderResourceViews = new ShaderResourceView[0];
        private SamplerState[] _samplerStates = new SamplerState[0];
        
        [Input(Guid = "B8E07473-60F9-4F5E-995D-7165EF8F7993")]
        public readonly InputSlot<InputLayout> InputLayout = new InputSlot<InputLayout>();
        [Input(Guid = "1EA95430-B853-4A60-A981-F316905995E8")]
        public readonly InputSlot<PrimitiveTopology> PrimitiveTopology = new InputSlot<PrimitiveTopology>();
        [Input(Guid = "4A1703D4-5958-4EDE-A755-79A12FE85F3B")]
        public readonly MultiInputSlot<Buffer> VertexBuffers = new MultiInputSlot<Buffer>();
        [Input(Guid = "C8FD1C4B-E6D6-4CA1-A718-4518E3BFFC59")]
        public readonly InputSlot<Buffer> IndexBuffer = new InputSlot<Buffer>();
    }
}