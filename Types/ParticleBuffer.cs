using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types
{
    public class ParticleBuffer : Instance<ParticleBuffer>
    {
        [Output(Guid = "FECD0B22-F28E-4EF9-80E4-76ED1EFE973C")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        public ParticleBuffer()
        {
            Buffer.UpdateAction = Update;
            // Buffer.DirtyFlag.Trigger = DirtyFlagTrigger.Always; // for debugging with renderdoc
        }

        private static int _seed = 19;
        private void Update(EvaluationContext context)
        {
            int count = Count.GetValue(context);
            var bufferContent = new BufferLayout[count];
            var rand = new System.Random(_seed++);
            for (int i = 0; i < count; i++)
            {
                bufferContent[i].Position = new Vector3(((float)rand.NextDouble() - 0.5f) * 200.0f,
                                                        ((float)rand.NextDouble() - 0.5f) * 200.0f,
                                                        ((float)rand.NextDouble() - 0.5f) * 200.0f);
                bufferContent[i].Lifetime = (float)rand.NextDouble() * 10.0f;
                bufferContent[i].Velocity = new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
                bufferContent[i].Dummy = 0.0f;
                bufferContent[i].Color = new Vector4((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
            }

            ResourceManager.Instance().SetupStructuredBuffer(bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(ParticleBuffer);
        }

        [StructLayout(LayoutKind.Explicit, Size = 48)]
        public struct BufferLayout
        {
            [FieldOffset(0)]
            public Vector3 Position;
            [FieldOffset(12)]
            public float Lifetime;
            [FieldOffset(16)]
            public Vector3 Velocity;
            [FieldOffset(28)]
            public float Dummy;
            [FieldOffset(32)]
            public Vector4 Color;
        }       
        
        [Input(Guid = "61D1BE34-26CF-43DB-9219-7A97AB3113B8")]
        public readonly InputSlot<int> Count = new InputSlot<int>(1000);
    }
}