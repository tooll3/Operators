using System.Diagnostics;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Device = SharpDX.Direct3D11.Device;

namespace T3.Operators.Types.Id_f9fe78c5_43a6_48ae_8e8c_6cdbbc330dd1
{
    public class RenderTarget : Instance<RenderTarget>
    {
        [Output(Guid = "7A4C4FEB-BE2F-463E-96C6-CD9A6BAD77A2")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        public RenderTarget()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;
            var clear = Clear.GetValue(context);

            Size2 size = Resolution.GetValue(context);
            if (size.Width == 0 || size.Height == 0)
            {
                size = context.RequestedResolution;

            }

            if (size.Width <= 0 || size.Height <= 0)
            {
                Log.Warning("Invalid texture size:" + size);
                return;
            }

            var wasRebuild = UpdateTextures(device, size, TextureFormat.GetValue(context));

            var deviceContext = device.ImmediateContext;
            var prevViewports = deviceContext.Rasterizer.GetViewports<RawViewportF>();
            var prevTargets = deviceContext.OutputMerger.GetRenderTargets(1);
            deviceContext.Rasterizer.SetViewport(new SharpDX.Viewport(0, 0, size.Width, size.Height, 0.0f, 1.0f));
            deviceContext.OutputMerger.SetTargets(_colorBufferRtv);
            var c = ClearColor.GetValue(context);
            if (clear || !_wasCleared)
            {
                deviceContext.ClearRenderTargetView(_colorBufferRtv, new Color(c.X, c.Y, c.Z, c.W));
                _wasCleared = true;
            }

            //context.CameraTworld = Matrix.Identity;
            var keepWorldTobject = context.WorldTobject;
            context.WorldTobject = Matrix.Identity;
            Command.GetValue(context);
            context.WorldTobject = keepWorldTobject;

            deviceContext.Rasterizer.SetViewports(prevViewports);
            deviceContext.OutputMerger.SetTargets(prevTargets);

            // clean up ref counts for RTVs
            for (int i = 0; i < prevTargets.Length; i++)
            {
                prevTargets[i].Dispose();
            }

            Output.Value = _colorBuffer;
        }

        private bool UpdateTextures(Device device, Size2 size, Format format)
        {
            if (_colorBuffer != null
                && _colorBuffer.Description.Width == size.Width
                && _colorBuffer.Description.Height == size.Height
                && _colorBuffer.Description.Format == format)
                return false; // nothing changed

            _colorBuffer?.Dispose();
            _colorBufferSrv?.Dispose();
            _colorBufferRtv?.Dispose();

            var colorDesc = new Texture2DDescription()
                            {
                                ArraySize = 1,
                                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                                CpuAccessFlags = CpuAccessFlags.None,
                                Format = format,
                                Width = size.Width,
                                Height = size.Height,
                                MipLevels = 1,
                                OptionFlags = ResourceOptionFlags.None,
                                SampleDescription = new SampleDescription(1, 0),
                                Usage = ResourceUsage.Default
                            };
            _colorBuffer = new Texture2D(device, colorDesc);
            _colorBufferSrv = new ShaderResourceView(device, _colorBuffer);
            _colorBufferRtv = new RenderTargetView(device, _colorBuffer);
            _wasCleared = false;
            return true;
        }

        private Texture2D _colorBuffer;
        private ShaderResourceView _colorBufferSrv;
        private bool _wasCleared;

        private RenderTargetView _colorBufferRtv;
        // private Texture2D _depthBuffer;
        // private DepthStencilView _depthBufferDsv;

        [Input(Guid = "4DA253B7-4953-439A-B03F-1D515A78BDDF")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();

        [Input(Guid = "03749B41-CC3C-4F38-AEA6-D7CEA19FC073")]
        public readonly InputSlot<Size2> Resolution = new InputSlot<Size2>();

        [Input(Guid = "8BB4A4E5-0C88-4D99-A5B2-2C9E22BD301F")]
        public readonly InputSlot<System.Numerics.Vector4> ClearColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "EC46BEF4-8DCE-4EB4-BFE8-E35A5AC416EC")]
        public readonly InputSlot<Format> TextureFormat = new InputSlot<Format>();

        [Input(Guid = "AACAFC4D-F47F-4893-9A6E-98DB306A8901")]
        public readonly InputSlot<bool> Clear = new InputSlot<bool>();

        // [Input(Guid = "2ac5ac30-6298-45a9-805b-326b933826fd")]
        // public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        // [Input(Guid = "")]
        // public readonly InputSlot<System.Numerics.Vector3> Scale = new InputSlot<System.Numerics.Vector3>();
    }
}