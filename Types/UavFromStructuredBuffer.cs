using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class UavFromStructuredBuffer : Instance<UavFromStructuredBuffer>
    {
        [Output(Guid = "7C9A5943-3DEB-4400-BDB2-99F56DD1976C")]
        public readonly Slot<UnorderedAccessView> UnorderedAccessView = new Slot<UnorderedAccessView>();

        public UavFromStructuredBuffer()
        {
            UnorderedAccessView.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var buffer = Buffer.GetValue(context);
            if (buffer != null)
            {
                if ((buffer.Description.OptionFlags & ResourceOptionFlags.BufferStructured) == 0)
                {
                    Log.Warning($"{nameof(UavFromStructuredBuffer)} - input buffer is not structured, skipping SRV creation.");
                    return;
                }

                UnorderedAccessView.Value?.Dispose();
                var desc = new UnorderedAccessViewDescription()
                           {
                               Dimension = UnorderedAccessViewDimension.Buffer,
                               Format = Format.Unknown,
                               Buffer = new UnorderedAccessViewDescription.BufferResource()
                                        {
                                            FirstElement = 0,
                                            ElementCount = buffer.Description.SizeInBytes / buffer.Description.StructureByteStride,
                                            Flags = BufferFlags.GetValue(context)
                                        }
                           };
                UnorderedAccessView.Value = new UnorderedAccessView(resourceManager._device, buffer, desc); // todo: create via resource manager
            }
        }

        [Input(Guid = "5d888f13-0ad8-4034-99ca-da36c8fb261c")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> Buffer = new InputSlot<SharpDX.Direct3D11.Buffer>();
        [Input(Guid = "13B85721-7126-47BB-AB4F-096EAE59E412")]
        public readonly InputSlot<UnorderedAccessViewBufferFlags> BufferFlags = new InputSlot<UnorderedAccessViewBufferFlags>();
    }
}