using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class TestParticleBuffer : Instance<TestParticleBuffer>
    {
        [Output(Guid = "FECD0B22-F28E-4EF9-80E4-76ED1EFE973C")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        public TestParticleBuffer()
        {
            Buffer.UpdateAction = Update;
//            Buffer.DirtyFlag.Trigger = DirtyFlagTrigger.Always; // for debugging with renderdoc
        }

        private void Update(EvaluationContext context)
        {
            int count = Count.GetValue(context);
            var bufferContent = new BufferLayout[count];
            var rand = new System.Random(0);
            for (int i = 0; i < count; i++)
            {
                bufferContent[i].Position = new Vector3(((float)rand.NextDouble() - 0.5f) * 200.0f,
                                                        ((float)rand.NextDouble() - 0.5f) * 200.0f,
                                                        ((float)rand.NextDouble() - 0.5f) * 200.0f);

                Vector3 dir = new Vector3((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
                dir.Normalize();
                bufferContent[i].Direction = dir;
            }

            ResourceManager.Instance().SetupStructuredBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(TestParticleBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct BufferLayout
        {
            [FieldOffset(0)]
            public Vector3 Position;
            [FieldOffset(16)]
            public Vector3 Direction;
        }       
        
        [Input(Guid = "61D1BE34-26CF-43DB-9219-7A97AB3113B8")]
        public readonly InputSlot<int> Count = new InputSlot<int>(1000);
    }
}