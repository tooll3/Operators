using System;
using System.Numerics;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class FloatConstBuffer : Instance<FloatConstBuffer>
    {
        [Output(Guid = "f5531ffb-dbde-45d3-af2a-bd90bcbf3710")]
        public readonly Slot<Buffer> Buffer = new Slot<Buffer>();

        public FloatConstBuffer()
        {
            Buffer.UpdateAction = Update;
            Buffer.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var param = Params.GetCollectedTypedInputs();
            int arraySize = (param.Count / 4 + (param.Count % 4 == 0 ? 0 : 1)) * 4; // always 16byte slices for alignment
            var array = new float[arraySize];

            if (array.Length == 0)
                return;
            
            for (int i = 0; i < param.Count; i++)
            {
                array[i]= param[i].GetValue(context);
            }
            
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;

            var size = sizeof(float)*array.Length;
            using (var data = new DataStream(size, true, true))
            {
                data.WriteRange(array);
                data.Position = 0;

                if (Buffer.Value == null)
                {
                    var bufferDesc = new BufferDescription
                                     {
                                         Usage = ResourceUsage.Default,
                                         SizeInBytes = size,
                                         BindFlags = BindFlags.ConstantBuffer
                                     };
                    Buffer.Value = new Buffer(device, data, bufferDesc);
                }
                else
                {
                    device.ImmediateContext.UpdateSubresource(new DataBox(data.DataPointer, 0, 0), Buffer.Value, 0);
                }
            }
            
            Buffer.Value.DebugName = nameof(FloatConstBuffer);
        }

        [Input(Guid = "49556D12-4CD1-4341-B9D8-C356668D296C")]
        public readonly MultiInputSlot<float> Params = new MultiInputSlot<float>();
    }
}