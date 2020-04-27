using System;
using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_e6f2a00d_854e_412e_94a1_a21df91fc988
{
    public class ShadowMapTransformsConstBuffer : Instance<ShadowMapTransformsConstBuffer>
    {
        [Output(Guid = "9b622d38-52e8-4a6d-8c03-4fc6eb8051b0", DirtyFlagTrigger = DirtyFlagTrigger.Always)]
        public readonly Slot<Buffer> Buffer = new Slot<Buffer>();

        [Input(Guid = "94d229a4-8bbf-4be4-a2f1-9b52cc785490")]
        public readonly InputSlot<System.Numerics.Vector3> Position = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "14038263-253a-4025-a7e5-fd6ca7dfb949")]
        public readonly InputSlot<System.Numerics.Vector3> Target = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "5783c091-816e-43a7-94ba-2e0df6380b0d")]
        public readonly InputSlot<System.Numerics.Vector2> NearFarClip = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "82c12036-6f0e-4da6-913c-0a2c37919466")]
        public readonly InputSlot<System.Numerics.Vector2> Size = new InputSlot<System.Numerics.Vector2>();

        public ShadowMapTransformsConstBuffer()
        {
            Buffer.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            System.Numerics.Vector2 size = Size.GetValue(context);
            System.Numerics.Vector2 clip = NearFarClip.GetValue(context);
            Matrix clipSpaceTcamera = Matrix.OrthoRH(size.X, size.Y, clip.X, clip.Y);
            clipSpaceTcamera.Transpose();
            
            var pos = Position.GetValue(context);
            Vector3 eye = new Vector3(pos.X, pos.Y, pos.Z);
            var t = Target.GetValue(context);
            Vector3 target = new Vector3(t.X, t.Y, t.Z);
            Vector3 viewDir = target - eye;
            viewDir.Normalize();
            Vector3 upRef = (Math.Abs(Vector3.Dot(viewDir, Vector3.Up)) > 0.9) ? Vector3.Left : Vector3.Up;
            Vector3 up = Vector3.Cross(upRef, target - eye);
            Matrix cameraTworld = Matrix.LookAtRH(eye, target, up);
            cameraTworld.Transpose();

            var bufferContent = new BufferLayout(clipSpaceTcamera, cameraTworld, context.WorldTobject);
            ResourceManager.Instance().SetupConstBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(ShadowMapTransformsConstBuffer);
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

                // transpose all as mem layout in hlsl constant buffer is row based
                ClipSpaceTcamera.Transpose();
                CameraTclipSpace.Transpose();
                CameraTworld.Transpose();
                WorldTcamera.Transpose();
                ClipSpaceTworld.Transpose();
                WorldTclipSpace.Transpose();
                WorldTobject.Transpose();
                ObjectTworld.Transpose();
                CameraTobject.Transpose();
                ClipSpaceTobject.Transpose();
            }

            [FieldOffset(0)]
            public Matrix ClipSpaceTcamera;
            [FieldOffset(64)]
            public Matrix CameraTclipSpace;
            [FieldOffset(128)]
            public Matrix CameraTworld;
            [FieldOffset(192)]
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