using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class SrvFromStructuredBuffer : Instance<SrvFromStructuredBuffer>
    {
        [Output(Guid = "2A1FCDF6-9416-45B2-96CA-A9D6D2692278")]
        public readonly Slot<ShaderResourceView> ShaderResourceView = new Slot<ShaderResourceView>();

        public SrvFromStructuredBuffer()
        {
            ShaderResourceView.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var buffer = Buffer.GetValue(context);
            if (buffer != null)
            {
                if ((buffer.Description.OptionFlags & ResourceOptionFlags.BufferStructured) == 0)
                {
                    Log.Warning($"{nameof(SrvFromStructuredBuffer)} - input buffer is not structured, skipping SRV creation.");
                    return;
                }
                ShaderResourceView.Value?.Dispose();
                var desc = new ShaderResourceViewDescription()
                           {
                               Dimension = ShaderResourceViewDimension.ExtendedBuffer,
                               Format = Format.Unknown,
                               BufferEx = new ShaderResourceViewDescription.ExtendedBufferResource()
                                          {
                                              FirstElement = 0,
                                              ElementCount = buffer.Description.SizeInBytes / buffer.Description.StructureByteStride
                                          }
                           };
                ShaderResourceView.Value = new ShaderResourceView(resourceManager._device, buffer, desc); // todo: create via resource manager
            }
        }

        [Input(Guid = "BD65EF2C-F32A-4279-BB5C-7F6467B23283")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> Buffer = new InputSlot<SharpDX.Direct3D11.Buffer>();
    }
}