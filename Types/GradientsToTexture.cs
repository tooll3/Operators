using System.Diagnostics;
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core.Animation;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Utilities = T3.Core.Utilities;

namespace T3.Operators.Types.Id_2c53eee7_eb38_449b_ad2a_d7a674952e5b
{
    public class GradientsToTexture : Instance<GradientsToTexture>
    {
        [Output(Guid = "7ad741ec-274d-493c-994f-1a125b96a6e9")]
        public readonly Slot<Texture2D> CurveTexture = new Slot<Texture2D>();

        

        public GradientsToTexture()
        {
            CurveTexture.UpdateAction = Update;
        }

        private float[] _floatBuffer = new float[0];
        
        private void Update(EvaluationContext context)
        {
            
            if (!Curves.IsConnected)
                return;
            
            var curveCount = Curves.CollectedInputs.Count;
            if (curveCount == 0)
                return;
            
            const int sampleCount = 256;
            const int entrySizeInBytes = sizeof(float);
            const int curveSizeInBytes = sampleCount * entrySizeInBytes;
            int bufferSizeInBytes = curveCount * curveSizeInBytes;

            using (var dataStream = new DataStream(bufferSizeInBytes, true, true))
            {
                var texDesc = new Texture2DDescription()
                                  {
                                      Width = sampleCount,
                                      Height = curveCount,
                                      ArraySize = 1,
                                      BindFlags = BindFlags.ShaderResource,
                                      Usage = ResourceUsage.Default,
                                      MipLevels = 1,
                                      CpuAccessFlags = CpuAccessFlags.None,
                                      Format = Format.R32_Float,
                                      SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                                  };

                foreach (var curveInput in Curves.CollectedInputs)
                {
                    var curve = curveInput.GetValue(context);
                    if (curve == null)
                    {
                        dataStream.Seek(curveSizeInBytes, SeekOrigin.Current);
                        continue;
                    }

                    for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
                    {
                        dataStream.Write((float)curve.GetSampledValue((float)sampleIndex / sampleCount));
                    }
                }
                //Curves.DirtyFlag.Clear();

                dataStream.Position = 0;
                var dataRectangles = new DataRectangle[] { new DataRectangle(dataStream.DataPointer, curveSizeInBytes) };
                Utilities.Dispose(ref CurveTexture.Value);
                CurveTexture.Value = new Texture2D(ResourceManager.Instance().Device, texDesc, dataRectangles);
            }
        }


        [Input(Guid = "1a1f3d10-fe5e-43bc-b02b-ad5939ec68ee")]
        public readonly MultiInputSlot<Curve> Curves = new MultiInputSlot<T3.Core.Animation.Curve>();
    }
}