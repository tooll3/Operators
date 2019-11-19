using System;
using SharpDX;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Camera : Instance<Camera>
    {
        [Output(Guid = "2E1742D8-9BA3-4236-A0CD-A2B02C9F5924")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Camera()
        {
            Output.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            float fov = (float)Math.PI / 4.0f;
            float aspectRatio = 16.0f / 9.0f;
            float zNear = 0.1f;
            float zFar = 1000.0f;
            Matrix clipSpaceTcamera = Matrix.PerspectiveFovRH(fov, aspectRatio, zNear, zFar);
            Matrix cameraTclipSpace = clipSpaceTcamera;
            cameraTclipSpace.Invert();
            Vector3 eye = new Vector3(0.0f, 0.0f, -10.0f);
            Vector3 target = Vector3.Zero;
            Matrix cameraTworld = Matrix.LookAtRH(eye, target, Vector3.Up);
            Matrix worldTcamera = cameraTworld;
            worldTcamera.Invert();
            Matrix clipSpaceTworld = clipSpaceTcamera * cameraTworld;
            Matrix worldTclipSpace = worldTcamera * cameraTclipSpace;

            context.ClipSpaceTcamera = clipSpaceTcamera;
            context.CameraTclipSpace = cameraTclipSpace;
            context.CameraTworld = cameraTworld;
            context.WorldTcamera = worldTcamera;
            context.ClipSpaceTworld = clipSpaceTworld;
            context.WorldTclipSpace = worldTclipSpace;
        }

        [Input(Guid = "047B8FAE-468C-48A7-8F3A-5FAC8DD5B3C6")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();
    }
}