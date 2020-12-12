using System;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_8f696d89_a23f_42ae_b382_8670febb546b 
{
    public class InitCATransitionBuffer : Instance<InitCATransitionBuffer>
    {
        [Output(Guid = "B0F31CB0-3D9F-426F-8E57-AAF94A5C8720")]
        public readonly Slot<BufferWithViews> OutBuffer = new Slot<BufferWithViews>();

        [Output(Guid = "773C1811-203D-42CB-A84F-F9692FAAF1EF")]
        public readonly Slot<int> TableLength = new Slot<int>();

        
        public InitCATransitionBuffer()
        {
            OutBuffer.UpdateAction = Update;
            TableLength.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var neighbourCount = NeighbourCount.GetValue(context).Clamp(1,9);
            var stateCount = StateCount.GetValue(context).Clamp(1,100);

            var bitsPerState = (int)(Math.Ceiling(Math.Log(stateCount, 2)));
            var ruleTableLength = 1 << bitsPerState * neighbourCount;
            var seed = RandomSeed.GetValue(context);
            
            var resourceManager = ResourceManager.Instance();
            if (_cellBuffer.Length != ruleTableLength)
            {
                _cellBuffer = new Cell[ruleTableLength];
            }

            var rand = new Random(seed);
            for (var ruleIndex = 0; ruleIndex < ruleTableLength; ruleIndex++)
            {
                _cellBuffer[ruleIndex] =  new Cell((uint)(rand.Next(stateCount) % stateCount));
            }

            const int stride = 4;

            _bufferWithViews.Buffer = _buffer;
            resourceManager.SetupStructuredBuffer(_cellBuffer, stride * _cellBuffer.Length, stride, ref _buffer);
            resourceManager.CreateStructuredBufferSrv(_buffer, ref _bufferWithViews.Srv);
            resourceManager.CreateStructuredBufferUav(_buffer, UnorderedAccessViewBufferFlags.None, ref _bufferWithViews.Uav);
            OutBuffer.Value = _bufferWithViews;
            TableLength.Value = ruleTableLength;
        }
        
        private Cell[] _cellBuffer = new Cell[0];
        private Buffer _buffer;
        private readonly BufferWithViews _bufferWithViews = new BufferWithViews();

        [StructLayout(LayoutKind.Explicit, Size = 4)]
        public struct Cell
        {
            public Cell(uint state)
            {
                State = state;
            }

            [FieldOffset(0)]
            public uint State;
        }       
        
        [Input(Guid = "42C3E0A8-5C19-4037-91E9-2B55196F5DE3")]
        public readonly InputSlot<int> StateCount = new InputSlot<int>();
        
        [Input(Guid = "8E58A20D-B260-43BD-BE1C-9C2D3F76A715")]
        public readonly InputSlot<int> NeighbourCount = new InputSlot<int>();
        
        [Input(Guid = "0ED09B7A-F9D3-4C81-886A-73503A6D783C")]
        public readonly InputSlot<int> RandomSeed = new InputSlot<int>();

    }
}
