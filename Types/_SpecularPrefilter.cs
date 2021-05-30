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
using Utilities = T3.Core.Utilities;

namespace T3.Operators.Types.Id_cc3cc712_9e87_49c6_b04b_49a12cf2ba75
{
    public class _SpecularPrefilter : Instance<_SpecularPrefilter>
    {
        [Output(Guid = "5dab6e1b-6136-45a9-bd63-1e18eafc20b7")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        public _SpecularPrefilter()
        {
            Output.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            /*
            var CubeMapSrc = CubeMap.GetValue(context); // Needs to be checked for null!

            // if (_prefilteredCubeMap != null && !Changed)
            // {
            //     context.Image = _prefilteredCubeMap;
            //     return context;
            // }

            Vector2 cubeMapSize = new Vector2(CubeMapSrc.Description.Width, CubeMapSrc.Description.Height);
            Log.Debug($"source size: {CubeMapSrc.Description.Width} num mips in src: {CubeMapSrc.Description.MipLevels}");

            // if ( _prefilteredCubeMap == null )
            // {
            Log.Debug("create cubemap", SymbolChildId);
            var cubeMapDesc = new Texture2DDescription
                                  {
                                      BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                                      Format = CubeMapSrc.Description.Format,
                                      Width = (int)cubeMapSize.X,
                                      Height = (int)cubeMapSize.Y,
                                      MipLevels = CubeMapSrc.Description.MipLevels,
                                      SampleDescription = CubeMapSrc.Description.SampleDescription,
                                      Usage = ResourceUsage.Default,
                                      OptionFlags = ResourceOptionFlags.TextureCube | ResourceOptionFlags.GenerateMipMaps,
                                      CpuAccessFlags = CpuAccessFlags.None,
                                      ArraySize = 6
                                  };

            var device = ResourceManager.Instance().Device;
            _prefilteredCubeMap = new Texture2D(device, cubeMapDesc);

            
            var rastDesc = new RasterizerStateDescription
                               {
                                   FillMode = FillMode.Solid,
                                   CullMode = CullMode.None,
                                   IsDepthClipEnabled = false
                               };
            _rasterizerState = new RasterizerState(device, rastDesc);
            // }

            // var prevEffect = context.Effect;
            // var prevRTV = context.RenderTargetView;
            // var prevDTV = context.DepthStencilView;

            using (var CubeMapView = new ShaderResourceView(device, CubeMapSrc))
            {
                var cubeMapVariable = _effect.GetVariableByName("CubeMap").AsShaderResource();
                if (cubeMapVariable != null)
                {
                    cubeMapVariable.SetResource(CubeMapView);
                    //Log.Debug("cubemap source set to shader");
                }
            }

            context.D3DDevice.ImmediateContext.OutputMerger.BlendState = OperatorPartContext.DefaultRenderer.DisabledBlendState;
            context.D3DDevice.ImmediateContext.OutputMerger.DepthStencilState = OperatorPartContext.DefaultRenderer.DisabledDepthStencilState;

            var rtvDesc = new RenderTargetViewDescription()
                              {
                                  Dimension = RenderTargetViewDimension.Texture2DArray,
                                  Format = CubeMapSrc.Description.Format,
                                  Texture2DArray = new RenderTargetViewDescription.Texture2DArrayResource()
                                                       {
                                                           ArraySize = 6,
                                                           FirstArraySlice = 0,
                                                           MipSlice = 0
                                                       }
                              };

            int size = _prefilteredCubeMap.Description.Width;
            int numMipLevels = _prefilteredCubeMap.Description.MipLevels;
            int mipSlice = 0;
            //float roughness = 0.0f;
            while (mipSlice < numMipLevels)
            {
                Utilities.DisposeObj(ref _cubeMapRTV);
                rtvDesc.Texture2DArray.MipSlice = mipSlice;
                _cubeMapRTV = new RenderTargetView(device, _prefilteredCubeMap, rtvDesc);
                context.D3DDevice.ImmediateContext.OutputMerger.SetTargets(_cubeMapRTV, null);

                var viewport = new ViewportF(0.0f, 0.0f, size, size);
                context.D3DDevice.ImmediateContext.Rasterizer.SetViewports(new[] { viewport });
                context.D3DDevice.ImmediateContext.Rasterizer.State = _rasterizerState;
                context.D3DDevice.ImmediateContext.InputAssembler.InputLayout = context.InputLayout;
                context.D3DDevice.ImmediateContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                context.D3DDevice.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(null, 0, 0));
                float roughness = (float)mipSlice / (_prefilteredCubeMap.Description.MipLevels - 1);
                Log.Debug(this, "roughness: {0}", roughness);
                _effect.GetVariableByName("Roughness").AsScalar().Set(roughness);

                for (int i = 0; i < _samplingParameters.Length; ++i)
                {
                    int indexToUse = -1;
                    if (roughness == _samplingParameters[i].roughness)
                    {
                        indexToUse = i;
                    }

                    if (indexToUse == -1 && roughness < _samplingParameters[i].roughness)
                    {
                        indexToUse = i - 1;
                    }

                    if (indexToUse != -1)
                    {
                        var param = _samplingParameters[indexToUse];
                        Log.Debug(this, "base mip: {0}  num samples: {1}", param.baseMip, param.numSamples);
                        _effect.GetVariableByName("BaseMip").AsScalar().Set(param.baseMip);
                        _effect.GetVariableByName("NumSamples").AsScalar().Set(param.numSamples);
                        break;
                    }
                }

                var technique = _effect.GetTechniqueByIndex(0);
                technique.GetPassByIndex(0).Apply(context.D3DDevice.ImmediateContext);
                context.D3DDevice.ImmediateContext.Draw(6, 0);

                size /= 2;
                ++mipSlice;
            }

            // restore render targets and viewport
            context.D3DDevice.ImmediateContext.OutputMerger.SetTargets(context.DepthStencilView, context.RenderTargetView);

            context.Image = _prefilteredCubeMap;
            //Texture2D.ToFile(context.D3DDevice.ImmediateContext, _prefilteredCubeMap, ImageFileFormat.Dds, "CubeMapTest.dds");
            Changed = false;
            return context;
            */
        }

        public struct SamplingParameter
        {
            public SamplingParameter(float r, int b, int num)
            {
                roughness = r;
                baseMip = b;
                numSamples = num;
            }

            public float roughness;
            public int baseMip;
            public int numSamples;
        }

        // r=0     -> base_mip = 0  1 sample
        // r=0.125 -> base_mip = 0  500 samples
        // r=0.375 -> base_mip = 4  900 samples
        // r=0.5   -> base_mip = 5  200 samples
        // r=0.75  -> base_mip = 6  100 samples
        // r=0.99  -> base_mip = 8  10 samples
        SamplingParameter[] _samplingParameters = new SamplingParameter[]
                                                      {
                                                          new SamplingParameter(0, 0, 1),
                                                          new SamplingParameter(0.125f, 0, 500),
                                                          new SamplingParameter(0.375f, 5, 500),
                                                          new SamplingParameter(0.5f, 5, 200),
                                                          new SamplingParameter(0.75f, 6, 100),
                                                          new SamplingParameter(1.0f, 8, 10),
                                                      };

        protected override void Dispose(bool disposing)
        {
            Utilities.Dispose(ref _prefilteredCubeMap);
            Utilities.Dispose(ref _cubeMapRTV);
            Utilities.Dispose(ref _rasterizerState);
            base.Dispose();
        }

        Texture2D _prefilteredCubeMap;
        RenderTargetView _cubeMapRTV;
        RasterizerState _rasterizerState;

        [Input(Guid = "9f7926aa-ac69-4963-af1d-342ad06fc278")]
        public readonly InputSlot<Texture2D> CubeMap = new InputSlot<Texture2D>();
    }
}