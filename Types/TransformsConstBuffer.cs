using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class TransformsConstBuffer : Instance<TransformsConstBuffer>
    {
        [Output(Guid = "7A76D147-4B8E-48CF-AA3E-AAC3AA90E888")]
        public readonly Slot<Buffer> Buffer = new Slot<Buffer>();

        public TransformsConstBuffer()
        {
            Buffer.UpdateAction = Update;
            Buffer.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var bufferContent = new BufferLayout(context.ClipSpaceTcamera, context.CameraTclipSpace, context.CameraTworld, context.WorldTcamera,
                                                 context.ClipSpaceTworld, context.WorldTclipSpace);
            ResourceManager.Instance().SetupConstBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(TransformsConstBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 4*4*4*6)]
        public struct BufferLayout
        {
            public BufferLayout(Matrix clipSpaceTcamera, Matrix cameraTclipSpace, Matrix cameraTworld, Matrix worldTcamera, Matrix clipSpaceTworld,
                                Matrix worldTclipSpace)
            {
                ClipSpaceTcamera = clipSpaceTcamera;
                CameraTclipSpace = cameraTclipSpace;
                CameraTworld = cameraTworld;
                WorldTcamera = worldTcamera;
                ClipSpaceTworld = clipSpaceTworld;
                WorldTclipSpace = worldTclipSpace;
            }

            [FieldOffset(0)]
            public Matrix ClipSpaceTcamera;
            [FieldOffset(64)]
            public Matrix CameraTclipSpace;
            [FieldOffset(128)]
            public Matrix CameraTworld;
            [FieldOffset(196)]
            public Matrix WorldTcamera;
            [FieldOffset(256)]
            public Matrix ClipSpaceTworld;
            [FieldOffset(320)]
            public Matrix WorldTclipSpace;
        }
    }
}