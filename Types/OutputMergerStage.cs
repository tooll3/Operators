﻿using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class OutputMergerStage : Instance<OutputMergerStage>
    {
        [Output(Guid = "CEE8C3F0-64EA-4E4D-B967-EC7E3688DD03")]
        public readonly Slot<Command> Output = new Slot<Command>(new Command());

        public OutputMergerStage()
        {
            Output.UpdateAction = Update;
            Output.Value.RestoreAction = Restore;
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
            var outputMerger = deviceContext.OutputMerger;

            UpdateMultiInput(RenderTargetViews, ref _renderTargetViews, context);

            _prevRenderTargetViews = outputMerger.GetRenderTargets(_renderTargetViews.Length, out _prevDepthStencilView);
            _prevBlendState = outputMerger.GetBlendState(out _prevBlendFactor, out _prevSampleMask);
            outputMerger.SetRenderTargets(null, _renderTargetViews);
            outputMerger.BlendState = BlendState.GetValue(context);
        }

        private void Restore(EvaluationContext context)
        {
            var deviceContext = ResourceManager.Instance()._device.ImmediateContext;
            var outputMerger = deviceContext.OutputMerger;

            outputMerger.BlendState = _prevBlendState;
            outputMerger.SetRenderTargets(_prevDepthStencilView, _prevRenderTargetViews);
        }

        private RenderTargetView[] _renderTargetViews = new RenderTargetView[0];
        private RenderTargetView[] _prevRenderTargetViews;
        private DepthStencilView _prevDepthStencilView;
        private BlendState _prevBlendState;
        private RawColor4 _prevBlendFactor;
        private int _prevSampleMask;

        [Input(Guid = "394D374F-2125-4ECB-8A69-CC7B2C3C6CB7")]
        public readonly InputSlot<DepthStencilView> DepthStencilView = new InputSlot<DepthStencilView>();

        [Input(Guid = "9C131DA6-AD56-4E15-9730-754096B3B765")]
        public readonly MultiInputSlot<RenderTargetView> RenderTargetViews = new MultiInputSlot<RenderTargetView>();

        [Input(Guid = "0ED97939-643B-445E-879C-E18C4430AA03")]
        public readonly MultiInputSlot<UnorderedAccessView> UnorderedAccessViews = new MultiInputSlot<UnorderedAccessView>();

        [Input(Guid = "1D5FAAD5-3BE5-426C-B464-AD490EA3D1AA")]
        public readonly InputSlot<DepthStencilState> DepthStencilState = new InputSlot<DepthStencilState>();

        [Input(Guid = "6C7907D7-70F7-4DB7-83EA-22EE48610994")]
        public readonly InputSlot<int> DepthStencilReference = new InputSlot<int>();

        [Input(Guid = "E0BC9CF8-42C8-4632-B958-7A96F6D03BA2")]
        public readonly InputSlot<BlendState> BlendState = new InputSlot<BlendState>();

        [Input(Guid = "CCEE2EC3-586F-4396-8B20-CC99484E1B64")]
        public readonly InputSlot<System.Numerics.Vector4> BlendFactor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "03166157-1E18-4513-8AF5-398C6F4FCB1E")]
        public readonly InputSlot<int> BlendSampleMask = new InputSlot<int>();
    }
}