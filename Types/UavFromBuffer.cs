using System.Runtime.InteropServices;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class UavFromBuffer : Instance<UavFromBuffer>
    {
        [Output(Guid = "D7CF0DAE-FFB7-4408-A1EA-B0C1B4BC60C2")]
        public readonly Slot<UnorderedAccessView> UnorderedAccessView = new Slot<UnorderedAccessView>();

        public UavFromBuffer()
        {
            UnorderedAccessView.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();
            var buffer = Buffer.GetValue(context);
            if (buffer != null)
            {
                if ((buffer.Description.OptionFlags & ResourceOptionFlags.BufferStructured) != 0)
                {
                    Log.Warning($"{nameof(UavFromBuffer)} - input buffer is structured, skipping UAV creation.");
                    return;
                }

                UnorderedAccessView.Value?.Dispose();
                var desc = new UnorderedAccessViewDescription()
                           {
                               Dimension = UnorderedAccessViewDimension.Buffer,
                               Format = Format.R32_UInt,
                               Buffer = new UnorderedAccessViewDescription.BufferResource()
                                        {
                                            FirstElement = 0,
                                            ElementCount = buffer.Description.SizeInBytes / Marshal.SizeOf<uint>(),
                                            Flags = UnorderedAccessViewBufferFlags.None
                                        }
                           };
                UnorderedAccessView.Value = new UnorderedAccessView(resourceManager._device, buffer, desc); // todo: create via resource manager
            }
        }

        [Input(Guid = "58EBAE6E-7D8C-45A0-8266-8B71F601DA0A")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> Buffer = new InputSlot<SharpDX.Direct3D11.Buffer>();
    }
}