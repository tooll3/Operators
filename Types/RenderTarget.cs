using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using T3.Core;
using T3.Core.Animation;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Device = SharpDX.Direct3D11.Device;
using Utilities = SharpDX.Utilities;

namespace T3.Operators.Types.Id_f9fe78c5_43a6_48ae_8e8c_6cdbbc330dd1
{
    public class RenderTarget : Instance<RenderTarget>
    {
        [Output(Guid = "7A4C4FEB-BE2F-463E-96C6-CD9A6BAD77A2")]
        public readonly Slot<Texture2D> ColorBuffer = new Slot<Texture2D>();

        [Output(Guid = "8bb0b18f-4fad-4348-a4fa-95b40c4167a4")]
        public readonly Slot<Texture2D> DepthBuffer = new Slot<Texture2D>();
        
        [Output(Guid = "152312A6-729B-49CB-9AC5-A63105694A6B")]
        public readonly Slot<Texture2D> VelocityBuffer = new Slot<Texture2D>();
        public RenderTarget()
        {
            ColorBuffer.UpdateAction = Update;
            DepthBuffer.UpdateAction = Update;
            VelocityBuffer.UpdateAction = Update;
        }

        private const int MaximumTexture2DSize = SharpDX.Direct3D11.Resource.MaximumTexture2DSize;

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

            if (size.Width <= 0 || size.Height <= 0 || size.Width > MaximumTexture2DSize || size.Height > MaximumTexture2DSize)
            {
                Log.Warning("Invalid texture size: " + size);
                return;
            }

            bool generateMips = GenerateMips.GetValue(context);
            bool useVelocityBuffer = UseVelocityBuffer.GetValue(context);
            UpdateTextures(device, size, TextureFormat.GetValue(context), DepthFormat.GetValue(context), generateMips, useVelocityBuffer);

            var deviceContext = device.ImmediateContext;
            var prevViewports = deviceContext.Rasterizer.GetViewports<RawViewportF>();
            var prevTargets = deviceContext.OutputMerger.GetRenderTargets(2, out var prevDepthStencilView);
            deviceContext.Rasterizer.SetViewport(new SharpDX.Viewport(0, 0, size.Width, size.Height, 0.0f, 1.0f));
            deviceContext.OutputMerger.SetTargets(_depthBufferDsv, _colorBufferRtv, _velocityBufferRtv);
            var c = ClearColor.GetValue(context);
            if (clear || !_wasCleared)
            {
                try
                {
                    deviceContext.ClearRenderTargetView(_colorBufferRtv, new Color(c.X, c.Y, c.Z, c.W));
                    if (useVelocityBuffer)
                        deviceContext.ClearRenderTargetView(_velocityBufferRtv, Color.Black);
                    if (_depthBufferDsv != null)
                    {
                        deviceContext.ClearDepthStencilView(_depthBufferDsv, DepthStencilClearFlags.Depth, 1.0f, 0);
                    }

                    _wasCleared = true;
                }
                catch
                {
                    Log.Error($"{Parent.Symbol.Name}: Error clearing actual render target.");
                }
            }

            var prevObjectToWorld = context.ObjectToWorld;
            var prevWorldToCamera = context.WorldToCamera;
            var prevCameraToClipSpace = context.CameraToClipSpace;
            
            context.SetDefaultCamera();
            
            if (TextureReference.IsConnected)
            {
                var reference = TextureReference.GetValue(context);
                reference.ColorTexture = _colorBuffer;
                reference.DepthTexture = _depthBuffer;
            }
            
            Command.GetValue(context);

            if (generateMips)
            {
                deviceContext.GenerateMips(_colorBufferSrv);
            }

            context.ObjectToWorld = prevObjectToWorld;
            context.WorldToCamera = prevWorldToCamera;
            context.CameraToClipSpace = prevCameraToClipSpace;
            deviceContext.Rasterizer.SetViewports(prevViewports);
            deviceContext.OutputMerger.SetTargets(prevDepthStencilView, prevTargets);

            // clean up ref counts for RTVs
            for (int i = 0; i < prevTargets.Length; i++)
            {
                prevTargets[i]?.Dispose();
            }

            prevDepthStencilView?.Dispose();

            ColorBuffer.Value = _colorBuffer;
            ColorBuffer.DirtyFlag.Clear();
            DepthBuffer.Value = _depthBuffer;
            DepthBuffer.DirtyFlag.Clear();
            VelocityBuffer.Value = _velocityBuffer;
            VelocityBuffer.DirtyFlag.Clear();
        }

        private bool UpdateTextures(Device device, Size2 size, Format colorFormat, Format depthFormat, bool generateMips, bool useVelocityBuffer)
        {
            int w = Math.Max(size.Width, size.Height);
            int mipLevels = generateMips ? (int)MathUtils.Log2(w) + 1 : 1;
            // Log.Debug($"miplevel: {mipLevels}, w: {w}");
            bool colorBufferNeedsUpdate = _colorBuffer == null || _colorBuffer.Description.Width != size.Width ||
                                          _colorBuffer.Description.Height != size.Height || _colorBuffer.Description.Format != colorFormat ||
                                          _colorBuffer.Description.MipLevels != mipLevels;

            if (colorBufferNeedsUpdate)
            {
                Core.Utilities.Dispose(ref _colorBufferSrv);
                Core.Utilities.Dispose(ref _colorBufferRtv);
                Core.Utilities.Dispose(ref _colorBuffer);

                try
                {
                    var colorDesc = new Texture2DDescription()
                                        {
                                            ArraySize = 1,
                                            BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource | BindFlags.UnorderedAccess,
                                            CpuAccessFlags = CpuAccessFlags.None,
                                            Format = colorFormat,
                                            Width = size.Width,
                                            Height = size.Height,
                                            MipLevels = mipLevels,
                                            OptionFlags = generateMips ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                                            SampleDescription = new SampleDescription(1, 0),
                                            Usage = ResourceUsage.Default
                                        };

                    _colorBuffer = new Texture2D(device, colorDesc);
                    _colorBufferSrv = new ShaderResourceView(device, _colorBuffer);
                    _colorBufferRtv = new RenderTargetView(device, _colorBuffer);

                    _wasCleared = false;
                }
                catch
                {
                    Core.Utilities.Dispose(ref _colorBufferSrv);
                    Core.Utilities.Dispose(ref _colorBufferRtv);
                    Core.Utilities.Dispose(ref _colorBuffer);
                    Log.Error("Error creating color render target.");
                }
            }

            bool depthBufferNeedsUpdate = _depthBuffer == null || (_depthBuffer != null && depthFormat == Format.Unknown) ||
                                          _depthBuffer.Description.Width != size.Width || _depthBuffer.Description.Height != size.Height ||
                                          _depthBuffer.Description.Format != depthFormat;

            if (depthBufferNeedsUpdate)
            {
                Core.Utilities.Dispose(ref _depthBufferDsv);
                Core.Utilities.Dispose(ref _depthBuffer);

                if (depthFormat == Format.Unknown)
                    return true;

                try
                {
                    var depthDesc = new Texture2DDescription()
                                        {
                                            ArraySize = 1,
                                            BindFlags = BindFlags.DepthStencil | BindFlags.ShaderResource,
                                            CpuAccessFlags = CpuAccessFlags.None,
                                            Format = Format.R32_Typeless,
                                            Width = size.Width,
                                            Height = size.Height,
                                            MipLevels = 1,
                                            OptionFlags = ResourceOptionFlags.None,
                                            SampleDescription = new SampleDescription(1, 0),
                                            Usage = ResourceUsage.Default
                                        };
                    _depthBuffer = new Texture2D(device, depthDesc);
                    var depthViewDesc = new DepthStencilViewDescription()
                                            {
                                                Format = Format.D32_Float,
                                                Dimension = DepthStencilViewDimension.Texture2D
                                            };
                    _depthBufferDsv = new DepthStencilView(device, _depthBuffer, depthViewDesc);
                    //Log.Debug("new depth stencil view");
                    _wasCleared = false;
                }
                catch
                {
                    Core.Utilities.Dispose(ref _depthBufferDsv);
                    Core.Utilities.Dispose(ref _depthBuffer);
                    Log.Error("Error creating depth/stencil buffer.");
                }
            }

            bool velocityBufferNeedsUpdate = useVelocityBuffer && (_velocityBuffer == null ||
                                                                   _velocityBuffer.Description.Width != size.Width ||
                                                                   _velocityBuffer.Description.Height != size.Height);

            if (velocityBufferNeedsUpdate)
            {
                Core.Utilities.Dispose(ref _velocityBufferSrv);
                Core.Utilities.Dispose(ref _velocityBufferRtv);
                Core.Utilities.Dispose(ref _velocityBuffer);

                try
                {
                    var velocityDesc = new Texture2DDescription()
                                           {
                                               ArraySize = 1,
                                               BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource | BindFlags.UnorderedAccess,
                                               CpuAccessFlags = CpuAccessFlags.None,
                                               Format = Format.R8G8B8A8_SNorm,
                                               Width = size.Width,
                                               Height = size.Height,
                                               MipLevels = mipLevels,
                                               OptionFlags = generateMips ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                                               SampleDescription = new SampleDescription(1, 0),
                                               Usage = ResourceUsage.Default
                                           };
                    _velocityBuffer = new Texture2D(device, velocityDesc);
                    _velocityBufferSrv = new ShaderResourceView(device, _velocityBuffer);
                    _velocityBufferRtv = new RenderTargetView(device, _velocityBuffer);
                    Log.Debug("Created velocity buffer");
                }
                catch
                {
                    Core.Utilities.Dispose(ref _velocityBuffer);
                    Core.Utilities.Dispose(ref _velocityBufferSrv);
                    Core.Utilities.Dispose(ref _velocityBufferRtv);
                    Log.Error("Error creating velocity render target.");
                }
            }

            return true;
        }

        private Texture2D _colorBuffer;
        private ShaderResourceView _colorBufferSrv;
        private RenderTargetView _colorBufferRtv;
        
        private Texture2D _velocityBuffer;
        private ShaderResourceView _velocityBufferSrv;
        private RenderTargetView _velocityBufferRtv;
        
        private bool _wasCleared;

        private Texture2D _depthBuffer;
        private DepthStencilView _depthBufferDsv;

        
        [Input(Guid = "4da253b7-4953-439a-b03f-1d515a78bddf")]
        public readonly InputSlot<T3.Core.Command> Command = new InputSlot<T3.Core.Command>();

        [Input(Guid = "03749b41-cc3c-4f38-aea6-d7cea19fc073")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "8bb4a4e5-0c88-4d99-a5b2-2c9e22bd301f")]
        public readonly InputSlot<System.Numerics.Vector4> ClearColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "ec46bef4-8dce-4eb4-bfe8-e35a5ac416ec")]
        public readonly InputSlot<SharpDX.DXGI.Format> TextureFormat = new InputSlot<SharpDX.DXGI.Format>();

        [Input(Guid = "2d54adbb-04c7-4f92-babd-9822953aa4cd")]
        public readonly InputSlot<SharpDX.DXGI.Format> DepthFormat = new InputSlot<SharpDX.DXGI.Format>();

        [Input(Guid = "aacafc4d-f47f-4893-9a6e-98db306a8901")]
        public readonly InputSlot<bool> Clear = new InputSlot<bool>();

        [Input(Guid = "f0cf3325-4967-4419-9beb-036cd6dbfd6a")]
        public readonly InputSlot<bool> GenerateMips = new InputSlot<bool>();
        
        [Input(Guid = "07AD28AD-FF5F-4CA9-B7BB-F7F8B16A6434")]
        public readonly InputSlot<RenderTargetReference> TextureReference = new InputSlot<RenderTargetReference>();

        [Input(Guid = "537C9406-7C31-4A31-A0C4-647B3DBE9A0E")]
        public readonly InputSlot<bool> UseVelocityBuffer = new InputSlot<bool>();
    }
}