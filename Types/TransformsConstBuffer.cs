using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_a60adc26_d7c6_4615_af78_8d2d6da46b79
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
            var bufferContent = new BufferLayout(context.ClipSpaceTcamera, context.CameraTworld, context.WorldTobject);
            ResourceManager.Instance().SetupConstBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(TransformsConstBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 4*4*4*10)]
        public struct BufferLayout
        {
            public BufferLayout(Matrix clipSpaceTcamera, Matrix cameraTworld, Matrix worldTobject) 
            {
                Matrix cameraTclipSpace = clipSpaceTcamera;
                cameraTclipSpace.Invert();
                Matrix worldTcamera = cameraTworld;
                worldTcamera.Invert();
                Matrix objectTworld = worldTobject;
                objectTworld.Invert();
                ClipSpaceTcamera = clipSpaceTcamera;
                CameraTclipSpace = cameraTclipSpace;
                CameraTworld = cameraTworld;
                WorldTcamera = worldTcamera;
                ClipSpaceTworld = Matrix.Multiply(clipSpaceTcamera, cameraTworld);
                WorldTclipSpace = Matrix.Multiply(worldTcamera, cameraTclipSpace);
                WorldTobject = worldTobject;
                ObjectTworld = objectTworld;
                CameraTobject = Matrix.Multiply(cameraTworld, worldTobject);
                ClipSpaceTobject = Matrix.Multiply(clipSpaceTcamera, CameraTobject);
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
            [FieldOffset(384)]
            public Matrix WorldTobject;
            [FieldOffset(448)]
            public Matrix ObjectTworld;
            [FieldOffset(512)]
            public Matrix CameraTobject;
            [FieldOffset(576)]
            public Matrix ClipSpaceTobject;
        }
    }
}