using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_de8bc97a_8ef0_4d4a_9ffa_88046a2daf40
{
    public class TimeConstBuffer : Instance<TimeConstBuffer>
    {
        [Output(Guid = "{6C118567-8827-4422-86CC-4D4D00762D87}")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        public TimeConstBuffer()
        {
            Buffer.UpdateAction = Update;
            Buffer.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var bufferContent = new TimeBufferLayout(
                                                     (float)EvaluationContext.GlobalTime, 
                                                     (float)context.Time, 
                                                     (float)EvaluationContext.RunTime,
                                                     (float)EvaluationContext.BeatTime);
            ResourceManager.Instance().SetupConstBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(TimeConstBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct TimeBufferLayout
        {
            public TimeBufferLayout(float globalTime, float time, float runTime, float beatTime)
            {
                GlobalTime = globalTime;
                Time = time;
                RunTime = runTime;
                BeatTime = beatTime;
            }

            [FieldOffset(0)]
            public float GlobalTime;
            [FieldOffset(4)]
            public float Time;
            [FieldOffset(8)]
            public float RunTime;
            [FieldOffset(12)]
            public float BeatTime;
        }       
    }
}