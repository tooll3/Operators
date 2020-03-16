using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_fa45d013_5a1c_45a0_9b05_a4a4edfb06f9
{
    public class TypoGridBuffer : Instance<TypoGridBuffer>
    {
        [Output(Guid = "{6e6e8ce0-2b62-41f5-893d-9a20219faf82}")]
        public readonly Slot<SharpDX.Direct3D11.Buffer> Buffer = new Slot<SharpDX.Direct3D11.Buffer>();

        public TypoGridBuffer()
        {
            Buffer.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            int width = 8;
            int height = 8;
            int size = width * height;
            _bufferContent = new BufferLayout[size];
            
            for (int i = 0; i < size; i++)
            {
                _bufferContent[i] = new BufferLayout(Vector3.One*(float)i, Vector2.Zero, Vector2.One);                
            }
            
            ResourceManager.Instance().SetupStructuredBuffer(_bufferContent, ref Buffer.Value);
            Buffer.Value.DebugName = nameof(TypoGridBuffer);
        }

        private BufferLayout[] _bufferContent;

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct BufferLayout
        {
            public BufferLayout(Vector3 pos, Vector2 uv, Vector2 size)
            {
                Pos = pos;
                Uv = uv;
                Size = size;
            }

            [FieldOffset(0)]
            public Vector3 Pos;
            [FieldOffset(16)]
            public Vector2 Uv;
            [FieldOffset(24)]
            public Vector2 Size;
        }       
    }
}