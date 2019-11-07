﻿using System.Security.AccessControl;
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
        public readonly Slot<Command> Output = new Slot<Command>(new Command());

        public Rasterizer()
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
            var rasterizer = deviceContext.Rasterizer;

            _prevViewports = rasterizer.GetViewports<RawViewportF>();
            UpdateMultiInput(Viewports, ref _viewports, context);
            rasterizer.State = RasterizerState.GetValue(context);


            rasterizer.SetViewports(_viewports, _viewports.Length);
        }

        private void Restore(EvaluationContext context)
        {
            var deviceContext = ResourceManager.Instance()._device.ImmediateContext;
            var rasterizer = deviceContext.Rasterizer;
            rasterizer.SetViewports(_prevViewports, _prevViewports.Length);
        }

        private RawViewportF[] _viewports = new RawViewportF[0];
        private RawViewportF[] _prevViewports;

        [Input(Guid = "35A52074-1E82-4352-91C3-D8E464F73BC7")]
        public readonly InputSlot<SharpDX.Direct3D11.RasterizerState> RasterizerState = new InputSlot<SharpDX.Direct3D11.RasterizerState>();
        [Input(Guid = "73945E5D-3C3C-4742-B341-A061B0DC116F")]
        public readonly MultiInputSlot<RawViewportF> Viewports = new MultiInputSlot<RawViewportF>();
        [Input(Guid = "3F71BE22-9DC2-4E47-8B3A-1EF3C9ECBD9D")]
        public readonly MultiInputSlot<RawRectangle> ScissorRectangles = new MultiInputSlot<RawRectangle>();
    }
}