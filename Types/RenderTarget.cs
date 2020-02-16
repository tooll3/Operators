using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using T3.Core;
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

            Size2 size = Size.GetValue(context);
            if (size.Width == 0 || size.Height == 0)
                return;

            UpdateTextures(device, size);

            var deviceContext = device.ImmediateContext;
            var prevViewports = deviceContext.Rasterizer.GetViewports<RawViewportF>();
            var prevTargets = deviceContext.OutputMerger.GetRenderTargets(1);
            deviceContext.Rasterizer.SetViewport(new SharpDX.Viewport(0, 0, size.Width, size.Height, 0.0f, 1.0f));
            deviceContext.OutputMerger.SetTargets(_colorBufferRtv);
            var c = ClearColor.GetValue(context);
            deviceContext.ClearRenderTargetView(_colorBufferRtv, new Color(c.X, c.Y, c.Z, c.W));

            //context.CameraTworld = Matrix.Identity;
            var keepWorldTobject= context.WorldTobject;
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

        private void UpdateTextures(Device device, Size2 size)
        {
            if (_colorBuffer != null && _colorBuffer.Description.Width == size.Width && _colorBuffer.Description.Height == size.Height)
                return; // nothing changed

            _colorBuffer?.Dispose();
            _colorBufferSrv?.Dispose();
            _colorBufferRtv?.Dispose();

            var colorDesc = new Texture2DDescription()
                            {
                                ArraySize = 1,
                                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                                CpuAccessFlags = CpuAccessFlags.None,
                                Format = Format.R8G8B8A8_UNorm,
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
        }

        private Texture2D _colorBuffer;
        private ShaderResourceView _colorBufferSrv;

        private RenderTargetView _colorBufferRtv;
        // private Texture2D _depthBuffer;
        // private DepthStencilView _depthBufferDsv;

        [Input(Guid = "4DA253B7-4953-439A-B03F-1D515A78BDDF")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();

        [Input(Guid = "03749B41-CC3C-4F38-AEA6-D7CEA19FC073")]
        public readonly InputSlot<Size2> Size = new InputSlot<Size2>();

        [Input(Guid = "8BB4A4E5-0C88-4D99-A5B2-2C9E22BD301F")]
        public readonly InputSlot<System.Numerics.Vector4> ClearColor = new InputSlot<System.Numerics.Vector4>();

        // [Input(Guid = "")]
        // public readonly InputSlot<System.Numerics.Vector3> Scale = new InputSlot<System.Numerics.Vector3>();
    }
}