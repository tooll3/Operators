using System;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using T3.Core;
using T3.Core.DataTypes;
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
        public readonly Slot<Texture2D> ColorBuffer = new Slot<Texture2D>();

        [Output(Guid = "8bb0b18f-4fad-4348-a4fa-95b40c4167a4")]
        public readonly Slot<Texture2D> DepthBuffer = new Slot<Texture2D>();

        [Output(Guid = "152312A6-729B-49CB-9AC5-A63105694A6B")]
        public readonly Slot<Texture2D> VelocityBuffer = new Slot<Texture2D>();

        public RenderTarget()
        {
            ColorBuffer.UpdateAction = Update;
            DepthBuffer.UpdateAction = Update;
        }

        private const int MaximumTexture2DSize = SharpDX.Direct3D11.Resource.MaximumTexture2DSize;

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;

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

            _sampleCount = Multisampling.GetValue(context).Clamp(1, 32);

            bool generateMips = GenerateMips.GetValue(context);
            var withDepthBuffer = WithDepthBuffer.GetValue(context);
            
            UpdateTextures(device, size, TextureFormat.GetValue(context), withDepthBuffer ? Format.R32_Typeless : Format.Unknown, generateMips);

            var deviceContext = device.ImmediateContext;

            // Save settings in context
            var prevViewports = deviceContext.Rasterizer.GetViewports<RawViewportF>();
            var prevTargets = deviceContext.OutputMerger.GetRenderTargets(2, out var prevDepthStencilView);
            var prevObjectToWorld = context.ObjectToWorld;
            var prevWorldToCamera = context.WorldToCamera;
            var prevCameraToClipSpace = context.CameraToClipSpace;

            deviceContext.Rasterizer.SetViewport(new SharpDX.Viewport(0, 0, size.Width, size.Height, 0.0f, 1.0f));
            deviceContext.OutputMerger.SetTargets(_multiSampledDepthBufferDsv, _multiSampledColorBufferRtv);

            // Clear
            var clear = Clear.GetValue(context);
            var c = ClearColor.GetValue(context);
            if (clear || !_wasCleared)
            {
                try
                {
                    deviceContext.ClearRenderTargetView(_multiSampledColorBufferRtv, new Color(c.X, c.Y, c.Z, c.W));
                    if (_multiSampledDepthBufferDsv != null)
                    {
                        deviceContext.ClearDepthStencilView(_multiSampledDepthBufferDsv, DepthStencilClearFlags.Depth, 1.0f, 0);
                    }

                    _wasCleared = true;
                }
                catch
                {
                    Log.Error($"{Parent.Symbol.Name}: Error clearing actual render target.", SymbolChildId);
                }
            }

            // Set default values for new sub tree
            context.SetDefaultCamera();

            if (TextureReference.IsConnected)
            {
                var reference = TextureReference.GetValue(context);
                reference.ColorTexture = ColorTexture;
                reference.DepthTexture = DepthTexture;
            }

            // Render
            Command.GetValue(context);

            // Restore context
            context.ObjectToWorld = prevObjectToWorld;
            context.WorldToCamera = prevWorldToCamera;
            context.CameraToClipSpace = prevCameraToClipSpace;
            deviceContext.Rasterizer.SetViewports(prevViewports);
            deviceContext.OutputMerger.SetTargets(prevDepthStencilView, prevTargets);

            if (_sampleCount > 1)
            {
                try
                {
                    device.ImmediateContext.ResolveSubresource(_multiSampledColorBuffer,
                                                               0,
                                                               _resolvedColorBuffer, 0,
                                                               _resolvedColorBuffer.Description.Format);
                }
                catch (Exception e)
                {
                    Log.Error("resolving render target buffer failed:" + e.Message);
                }
            }

            // Resolve depth

            if (generateMips)
            {
                deviceContext.GenerateMips(DownSamplingRequired 
                                               ? _resolvedColorBufferSrv 
                                               : _multiSampledColorBufferSrv);
            }

            // Clean up ref counts for RTVs
            for (int i = 0; i < prevTargets.Length; i++)
            {
                prevTargets[i]?.Dispose();
            }

            prevDepthStencilView?.Dispose();

            ColorBuffer.Value = ColorTexture;
            ColorBuffer.DirtyFlag.Clear();
            DepthBuffer.Value = DepthTexture;
            DepthBuffer.DirtyFlag.Clear();
        }
        
        private uint _resolveComputeShaderResourceId = ResourceManager.NullResource;

        private void SetupShaderResources()
        {
            if (_resolveComputeShaderResourceId == ResourceManager.NullResource)
            {
                string sourcePath = @"Resources\lib\internal\resolve-multisampled-depth-buffer-cs.hlsl";
                string entryPoint = "main";
                string debugName = "resolve-multisampled-depth-buffer";
                var resourceManager = ResourceManager.Instance();
                _resolveComputeShaderResourceId = resourceManager.CreateVertexShaderFromFile(sourcePath, entryPoint, debugName, null);
            }
        }
        
        private void Resolve()
        {
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;
            var deviceContext = device.ImmediateContext;
            var csStage = deviceContext.ComputeShader;
            var prevShader = csStage.Get();
            var prevUavs = csStage.GetUnorderedAccessViews(0, 1);
            
            // Set and call shader
            var reflectedShader = new ShaderReflection(resourceManager.GetComputeShaderBytecode(_resolveComputeShaderResourceId));
            reflectedShader.GetThreadGroupSize(out int threadNumX, out int threadNumY, out int threadNumZ);
            
            ComputeShader resolveShader = resourceManager.GetComputeShader(_resolveComputeShaderResourceId);
            csStage.Set(resolveShader);
            
            // FIXME: implement UAV for _depthbuffer
            //csStage.SetUnorderedAccessView(0, _multiSampledDepthBufferDsv, 0);
            var dispatchCount = _multiSampledDepthBuffer.Description.Width / threadNumX * _multiSampledDepthBuffer.Description.Height / threadNumY;
            deviceContext.Dispatch(dispatchCount, 1, 1);
            
            // Restore prev setup
            csStage.SetUnorderedAccessView(0, prevUavs[0]);
            csStage.Set(prevShader);
        }
        
        
        private void UpdateTextures(Device device, Size2 size, Format colorFormat, Format depthFormat, bool generateMips)
        {
            int w = Math.Max(size.Width, size.Height);
            int mipLevels = generateMips ? (int)MathUtils.Log2(w) + 1 : 1;
            // Log.Debug($"miplevel: {mipLevels}, w: {w}");

            bool colorFormatChanged = _resolvedColorBuffer == null
                                      || _multiSampledColorBuffer == null
                                      || _multiSampledColorBuffer.Description.Format != colorFormat
                                      || _multiSampledColorBuffer.Description.MipLevels != mipLevels
                                      || _multiSampledColorBuffer.Description.Width != size.Width
                                      || _multiSampledColorBuffer.Description.Height != size.Height
                                      || _multiSampledColorBuffer.Description.SampleDescription.Count != _sampleCount;

            //bool useMultiSampling = _sampleCount > 1;

            if (colorFormatChanged)
            {
                // Color / Multi sampling
                Core.Utilities.Dispose(ref _multiSampledColorBufferSrv);
                Core.Utilities.Dispose(ref _multiSampledColorBufferRtv);
                Core.Utilities.Dispose(ref _multiSampledColorBuffer);

                try
                {
                    var texture2DDescription = new Texture2DDescription
                                                   {
                                                       ArraySize = 1,
                                                       BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                                                       CpuAccessFlags = CpuAccessFlags.None,
                                                       Format = colorFormat,
                                                       Width = size.Width,
                                                       Height = size.Height,
                                                       MipLevels = DownSamplingRequired ? 1 : mipLevels,
                                                       OptionFlags = ResourceOptionFlags.None,
                                                       SampleDescription = new SampleDescription(_sampleCount, 0),
                                                       Usage = ResourceUsage.Default,
                                                   };
                    _multiSampledColorBuffer = new Texture2D(device, texture2DDescription);

                    _multiSampledColorBufferSrv = new ShaderResourceView(device, _multiSampledColorBuffer);
                    _multiSampledColorBufferRtv = new RenderTargetView(device, _multiSampledColorBuffer,
                                                                       new RenderTargetViewDescription
                                                                           {
                                                                               Format = colorFormat,
                                                                               Dimension = DownSamplingRequired
                                                                                               ? RenderTargetViewDimension.Texture2DMultisampled
                                                                                               : RenderTargetViewDimension.Texture2D
                                                                           });

                    //_multiSampledColorBufferRtv = new RenderTargetView(device, _multiSampledColorBuffer);
                    _wasCleared = false;
                }
                catch (Exception e)
                {
                    Log.Error("Error creating color render target." + e.Message, SymbolChildId);
                    Core.Utilities.Dispose(ref _multiSampledColorBufferSrv);
                    Core.Utilities.Dispose(ref _multiSampledColorBufferRtv);
                    Core.Utilities.Dispose(ref _multiSampledColorBuffer);
                }

                // Color / Down sampled
                if (_resolvedColorBuffer != null)
                {
                    Core.Utilities.Dispose(ref _resolvedColorBufferSrv);
                    Core.Utilities.Dispose(ref _resolvedColorBufferRtv);
                    Core.Utilities.Dispose(ref _resolvedColorBuffer);
                }

                if (DownSamplingRequired)
                {
                    try
                    {
                        _resolvedColorBuffer = new Texture2D(device,
                                                             new Texture2DDescription
                                                                 {
                                                                     ArraySize = 1,
                                                                     BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource | BindFlags.UnorderedAccess,
                                                                     CpuAccessFlags = CpuAccessFlags.None,
                                                                     Format = colorFormat,
                                                                     Width = size.Width,
                                                                     Height = size.Height,
                                                                     MipLevels = mipLevels,
                                                                     OptionFlags =
                                                                         generateMips ? ResourceOptionFlags.GenerateMipMaps : ResourceOptionFlags.None,
                                                                     SampleDescription = new SampleDescription(1, 0),
                                                                     Usage = ResourceUsage.Default
                                                                 });
                        _resolvedColorBufferSrv = new ShaderResourceView(device, _resolvedColorBuffer);
                        _resolvedColorBufferRtv = new RenderTargetView(device, _resolvedColorBuffer);
                        _wasCleared = false;
                    }
                    catch
                    {
                        Log.Error("Error creating color render target.", SymbolChildId);
                        Core.Utilities.Dispose(ref _resolvedColorBufferSrv);
                        Core.Utilities.Dispose(ref _resolvedColorBufferRtv);
                        Core.Utilities.Dispose(ref _resolvedColorBuffer);
                    }
                }

                _wasCleared = false;
            }

            var depthRequired = depthFormat != Format.Unknown;
            var depthInitialized = _multiSampledDepthBuffer != null;

            bool depthFormatChanged = _multiSampledDepthBuffer == null
                                      || _multiSampledDepthBuffer.Description.Width != size.Width
                                      || _multiSampledDepthBuffer.Description.Height != size.Height
                                      || _multiSampledDepthBuffer.Description.SampleDescription.Count != _sampleCount
                                      || _multiSampledDepthBuffer.Description.Format != depthFormat;

            if (depthFormatChanged || (!depthRequired && depthInitialized))
            {
                Core.Utilities.Dispose(ref _multiSampledDepthBufferDsv);
                Core.Utilities.Dispose(ref _multiSampledDepthBuffer);
                Core.Utilities.Dispose(ref _resolvedDepthBufferDsv);
                Core.Utilities.Dispose(ref _resolvedDepthBuffer);
            }

            if (depthRequired && (depthFormatChanged || !depthInitialized))
            {
                // Depth / Multi sampled
                try
                {
                    _multiSampledDepthBuffer = new Texture2D(device,
                                                             new Texture2DDescription
                                                                 {
                                                                     ArraySize = 1,
                                                                     BindFlags = BindFlags.DepthStencil | BindFlags.ShaderResource,
                                                                     CpuAccessFlags = CpuAccessFlags.None,
                                                                     Format = Format.R32_Typeless,
                                                                     Width = size.Width,
                                                                     Height = size.Height,
                                                                     MipLevels = DownSamplingRequired ? 1: mipLevels,
                                                                     OptionFlags = ResourceOptionFlags.None,
                                                                     SampleDescription = new SampleDescription(_sampleCount, 0),
                                                                     Usage = ResourceUsage.Default
                                                                 });

                    _multiSampledDepthBufferDsv = new DepthStencilView(device,
                                                                       _multiSampledDepthBuffer,
                                                                       new DepthStencilViewDescription
                                                                           {
                                                                               Format = Format.D32_Float,
                                                                               Dimension = DownSamplingRequired
                                                                                               ? DepthStencilViewDimension.Texture2DMultisampled
                                                                                               : DepthStencilViewDimension.Texture2D
                                                                           });
                }
                catch
                {
                    Core.Utilities.Dispose(ref _multiSampledDepthBufferDsv);
                    Core.Utilities.Dispose(ref _multiSampledDepthBuffer);
                    Log.Error("Error  creating multisampled depth/stencil buffer.", SymbolChildId);
                }

                if (DownSamplingRequired)
                {
                    try
                    {
                        _resolvedDepthBuffer = new Texture2D(device,
                                                             new Texture2DDescription
                                                                 {
                                                                     ArraySize = 1,
                                                                     BindFlags = BindFlags.DepthStencil | BindFlags.ShaderResource,
                                                                     CpuAccessFlags = CpuAccessFlags.None,
                                                                     Format = Format.R32_Typeless,
                                                                     Width = size.Width,
                                                                     Height = size.Height,
                                                                     MipLevels = mipLevels,
                                                                     OptionFlags = ResourceOptionFlags.None,
                                                                     SampleDescription = new SampleDescription(1, 0),
                                                                     Usage = ResourceUsage.Default
                                                                 });

                        _resolvedDepthBufferDsv = new DepthStencilView(device, _resolvedDepthBuffer,
                                                                       new DepthStencilViewDescription
                                                                           {
                                                                               Format = Format.D32_Float,
                                                                               Dimension = DepthStencilViewDimension.Texture2D
                                                                           });
                    }
                    catch
                    {
                        Core.Utilities.Dispose(ref _resolvedDepthBufferDsv);
                        Core.Utilities.Dispose(ref _resolvedDepthBuffer);
                        Log.Error("Error creating depth/stencil downsampling buffer.", SymbolChildId);
                    }
                }
            }
        }

        private Texture2D _multiSampledColorBuffer;
        private ShaderResourceView _multiSampledColorBufferSrv;
        private RenderTargetView _multiSampledColorBufferRtv;

        private Texture2D _resolvedColorBuffer;
        private ShaderResourceView _resolvedColorBufferSrv;
        private RenderTargetView _resolvedColorBufferRtv;

        private Texture2D _multiSampledDepthBuffer;
        private DepthStencilView _multiSampledDepthBufferDsv;

        private Texture2D _resolvedDepthBuffer;
        private DepthStencilView _resolvedDepthBufferDsv;

        private bool _wasCleared;

        private Texture2D ColorTexture => _sampleCount > 1 ? _resolvedColorBuffer : _multiSampledColorBuffer ;
        private Texture2D DepthTexture => _sampleCount > 1 ? _resolvedDepthBuffer : _multiSampledDepthBuffer;
        private bool DownSamplingRequired => _sampleCount > 1;
        private int _sampleCount;

        [Input(Guid = "4da253b7-4953-439a-b03f-1d515a78bddf")]
        public readonly InputSlot<T3.Core.Command> Command = new InputSlot<T3.Core.Command>();

        [Input(Guid = "03749b41-cc3c-4f38-aea6-d7cea19fc073")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "8bb4a4e5-0c88-4d99-a5b2-2c9e22bd301f")]
        public readonly InputSlot<System.Numerics.Vector4> ClearColor = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "ec46bef4-8dce-4eb4-bfe8-e35a5ac416ec")]
        public readonly InputSlot<SharpDX.DXGI.Format> TextureFormat = new InputSlot<SharpDX.DXGI.Format>();

        // [Input(Guid = "2d54adbb-04c7-4f92-babd-9822953aa4cd")]
        // public readonly InputSlot<SharpDX.DXGI.Format> DepthFormat = new InputSlot<SharpDX.DXGI.Format>();
        
        [Input(Guid = "6EA4F801-FF52-4266-A41F-B9EF02C68510")]
        public readonly InputSlot<bool> WithDepthBuffer = new InputSlot<bool>();
        
        [Input(Guid = "aacafc4d-f47f-4893-9a6e-98db306a8901")]
        public readonly InputSlot<bool> Clear = new InputSlot<bool>();

        [Input(Guid = "f0cf3325-4967-4419-9beb-036cd6dbfd6a")]
        public readonly InputSlot<bool> GenerateMips = new InputSlot<bool>();

        [Input(Guid = "07AD28AD-FF5F-4CA9-B7BB-F7F8B16A6434")]
        public readonly InputSlot<RenderTargetReference> TextureReference = new InputSlot<RenderTargetReference>();

        [Input(Guid = "E882E0F0-03F9-46E6-AC7A-709E6FA66613")]
        public readonly InputSlot<int> Multisampling = new InputSlot<int>();
    }
}