using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core.Animation;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ab511978_bad5_4b69_90b2_c028447fe9f7
{
    public class CurvesToTexture : Instance<CurvesToTexture>
    {
        [Output(Guid = "0322FFC8-84BD-4AA3-A59E-DEF5B212D4A1")]
        public readonly Slot<Texture2D> CurveTexture = new Slot<Texture2D>();

        

        public CurvesToTexture()
        {
            CurveTexture.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (Curve == null)
                return;

            var u = U.GetValue(context);
            var c = Curve.GetValue(context);
            if (c == null)
                return;
            
            const int sampleCount = 256;
            var bufferData = new float[sampleCount];
            for (var index = 0; index < sampleCount; index++)
            {
                bufferData[index] = (float)c.GetSampledValue((float)index/sampleCount);
            }

            const int stride = 4;
            
            using (var buffer = new DataStream(sampleCount * stride, true, true))
            {
                var texDesc = new Texture2DDescription()
                                  {
                                      Width = sampleCount,
                                      Height = 1,
                                      ArraySize = 1,
                                      BindFlags = BindFlags.ShaderResource,
                                      Usage = ResourceUsage.Default,
                                      MipLevels = 1,
                                      CpuAccessFlags = CpuAccessFlags.None,
                                      Format = Format.R32_Float,
                                      SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                                  };
                var dataBoxArray = new DataBox[1] { new DataBox(buffer.DataPointer, sampleCount * stride, 0)};
                buffer.WriteRange(bufferData);
                buffer.Position = 0;
                var texture = new Texture2D(ResourceManager.Instance().Device, texDesc, dataBoxArray);
                CurveTexture.Value = texture;
            }
        }


        [Input(Guid = "83c5a68a-2685-4506-8d79-d0db7d739102")]
        public readonly InputSlot<Curve> Curve = new InputSlot<T3.Core.Animation.Curve>();

        [Input(Guid = "fcf47c84-f386-4099-a5ea-ffc32b476a44")]
        public readonly InputSlot<float> U = new InputSlot<float>();

        [Input(Guid = "4453e970-c803-44cc-b910-0c09bdf7aa85")]
        public readonly InputSlot<float> Input2 = new InputSlot<float>();
    }
}