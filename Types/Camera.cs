using System;
using SharpDX;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_746d886c_5ab6_44b1_bb15_f3ce2fadf7e6
{
    public class Camera : Instance<Camera>
    {
        [Output(Guid = "2E1742D8-9BA3-4236-A0CD-A2B02C9F5924", DirtyFlagTrigger = DirtyFlagTrigger.Always)]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Camera()
        {
            Output.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            float fov = MathUtil.DegreesToRadians(Fov.GetValue(context));
            float aspectRatio = AspectRatio.GetValue(context);
            System.Numerics.Vector2 clip = NearFarClip.GetValue(context);
            Matrix clipSpaceTcamera = Matrix.PerspectiveFovRH(fov, aspectRatio, clip.X, clip.Y);
            clipSpaceTcamera.Transpose();
            var pos = Position.GetValue(context);
            Vector3 eye = new Vector3(pos.X, pos.Y, pos.Z);
            var t = Target.GetValue(context);
            Vector3 target = new Vector3(t.X, t.Y, t.Z);
            var u = Up.GetValue(context);
            Vector3 up = new Vector3(u.X, u.Y, u.Z);
            Matrix cameraTworld = Matrix.LookAtRH(eye, target, up);
            cameraTworld.Transpose();

            var prevClipSpace = context.ClipSpaceTcamera; 
            context.ClipSpaceTcamera = clipSpaceTcamera;

            var prevCamToWorld = context.CameraTworld;
            context.CameraTworld = cameraTworld;
            Command.GetValue(context);
            
            context.ClipSpaceTcamera = prevClipSpace;
            context.CameraTworld = prevCamToWorld;
        }

        [Input(Guid = "047B8FAE-468C-48A7-8F3A-5FAC8DD5B3C6")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();
        [Input(Guid = "313596CC-3854-436B-89DA-5FD40164CE76")]
        public readonly InputSlot<System.Numerics.Vector3> Position = new InputSlot<System.Numerics.Vector3>();
        [Input(Guid = "A7ACB25C-D60C-43A6-B1DF-2CD5C6E183F3")]
        public readonly InputSlot<System.Numerics.Vector3> Target = new InputSlot<System.Numerics.Vector3>();
        [Input(Guid = "E6DFBFB9-EFED-4C17-8860-9C1A1CA2FA38")]
        public readonly InputSlot<System.Numerics.Vector3> Up = new InputSlot<System.Numerics.Vector3>();
        [Input(Guid = "F66E91A1-B991-48C3-A8C9-33BCAD0C2F6F")]
        public readonly InputSlot<float> AspectRatio = new InputSlot<float>();
        [Input(Guid = "199D4CE0-AAB1-403A-AD42-216EF1061A0E")]
        public readonly InputSlot<System.Numerics.Vector2> NearFarClip = new InputSlot<System.Numerics.Vector2>();
        [Input(Guid = "7BDE5A5A-CE82-4903-92FF-14E540A605F0")]
        public readonly InputSlot<float> Fov = new InputSlot<float>();
        [Input(Guid = "764CA304-FC86-48A9-9C82-A04FAC7EADB2")]
        public readonly InputSlot<float> Roll = new InputSlot<float>();
    }
}