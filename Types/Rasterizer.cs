using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class Rasterizer : Instance<Rasterizer>
    {
        [Output(Guid = "C723AD69-FF0C-47B2-9327-BD27C0D7B6D1")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Rasterizer()
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
            var rasterizer = deviceContext.Rasterizer;

//            UpdateMultiInput(ConstantBuffers, ref _constantBuffers, context);
//            UpdateMultiInput(ShaderResources, ref _shaderResourceViews, context);
//            UpdateMultiInput(SamplerStates, ref _samplerStates, context);
            UpdateMultiInput(Viewports, ref _viewports, context);
            rasterizer.State = RasterizerState.GetValue(context);


            rasterizer.SetViewports(_viewports, _viewports.Length);
//            rasterizer.SetScissorRectangles();

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

//        private Buffer[] _constantBuffers = new Buffer[0];
//        private ShaderResourceView[] _shaderResourceViews = new ShaderResourceView[0];
//        private SamplerState[] _samplerStates = new SamplerState[0];
        private RawViewportF[] _viewports = new RawViewportF[0];

        [Input(Guid = "35A52074-1E82-4352-91C3-D8E464F73BC7")]
        public readonly InputSlot<SharpDX.Direct3D11.RasterizerState> RasterizerState = new InputSlot<SharpDX.Direct3D11.RasterizerState>();
        [Input(Guid = "73945E5D-3C3C-4742-B341-A061B0DC116F")]
        public readonly MultiInputSlot<RawViewportF> Viewports = new MultiInputSlot<RawViewportF>();
        [Input(Guid = "3F71BE22-9DC2-4E47-8B3A-1EF3C9ECBD9D")]
        public readonly MultiInputSlot<RawRectangle> ScissorRectangles = new MultiInputSlot<RawRectangle>();
    }
}