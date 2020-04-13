using System;
using SharpDX;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_284d2183_197d_47fd_b130_873cced78b1c
{
    public class Transform : Instance<Transform>
    {
        [Output(Guid = "2D329133-29B9-4F56-B5A6-5FF7D83638FA")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Transform()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var s = Scale.GetValue(context);
            var r = Rotation.GetValue(context);
            float yaw = MathUtil.DegreesToRadians(r.Y);
            float pitch = MathUtil.DegreesToRadians(r.X);
            float roll = MathUtil.DegreesToRadians(r.Z);
            var t = Translation.GetValue(context);
            var worldTobject = Matrix.Transformation(Vector3.Zero, Quaternion.Identity, new Vector3(s.X, s.Y, s.Z), Vector3.Zero,
                                                         Quaternion.RotationYawPitchRoll(yaw, pitch, roll), new Vector3(t.X, t.Y, t.Z));
            worldTobject.Transpose();
            
            var previousWorldTobject = context.WorldTobject;
            context.WorldTobject = Matrix.Multiply(context.WorldTobject, worldTobject);
            Command.GetValue(context);
            context.WorldTobject = previousWorldTobject;
        }

        [Input(Guid = "DCD066CE-AC44-4E76-85B3-78821245D9DC")]
        public readonly InputSlot<Command> Command = new InputSlot<Command>();
        [Input(Guid = "B4A8C16D-5A0F-4867-AE03-92A675ABE709")]
        public readonly InputSlot<System.Numerics.Vector3> Translation = new InputSlot<System.Numerics.Vector3>();
        [Input(Guid = "712ADB09-D249-4C91-86DB-3FEDF6B05971")]
        public readonly InputSlot<System.Numerics.Vector3> Rotation = new InputSlot<System.Numerics.Vector3>();
        [Input(Guid = "DA4CD6C8-2307-45DA-9258-49C578025AA8")]
        public readonly InputSlot<System.Numerics.Vector3> Scale = new InputSlot<System.Numerics.Vector3>();
    }
}