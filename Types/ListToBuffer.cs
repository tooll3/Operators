using System;
using System.Linq;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_7e28c796_85e7_47ee_99bb_9599284dbeeb
{
    public class ListToBuffer : Instance<ListToBuffer>
    {
        [Output(Guid = "c52dfa83-9820-4a54-b90b-62278dc8fe3f")]
        public readonly Slot<BufferWithViews> OutBuffer = new Slot<BufferWithViews>();

        
        [Output(Guid = "e1775fdf-af5a-49b2-b6fc-20e2180b71f5")]
        public readonly Slot<int> Length = new Slot<int>();
        
        public ListToBuffer()
        {
            Length.UpdateAction = Update;
            OutBuffer.UpdateAction = Update;
        }


        private void Update(EvaluationContext context)
        {
            var listsCollectedInputs = Lists.CollectedInputs.Select(c => c.GetValue(context));
            Lists.DirtyFlag.Clear();
            
            var totalSizeInBytes = 0;
            foreach (var xxx in listsCollectedInputs)
            {
                totalSizeInBytes += xxx.TotalSizeInBytes;
            }

            var resourceManager = ResourceManager.Instance();
            using (var data = new DataStream(totalSizeInBytes, true, true))
            {
                foreach (var i in listsCollectedInputs)
                {
                    i.WriteToStream(data);
                }
                data.Position = 0;

                var firstInputList = listsCollectedInputs.FirstOrDefault();
                var elementSizeInBytes = firstInputList?.ElementSizeInBytes ?? 0; // todo: add check that all inputs have same type
                resourceManager.SetupStructuredBuffer(data, totalSizeInBytes, elementSizeInBytes, ref _buffer);
            }

            _bufferWithViews.Buffer = _buffer;
            resourceManager.CreateStructuredBufferSrv(_buffer, ref _bufferWithViews.Srv);
            resourceManager.CreateStructuredBufferUav(_buffer, UnorderedAccessViewBufferFlags.None, ref _bufferWithViews.Uav);
            OutBuffer.Value = _bufferWithViews;
        }

        private Buffer _buffer;
        private BufferWithViews _bufferWithViews = new BufferWithViews();

        [Input(Guid = "08F181BB-9777-497C-871D-BCC1FF252F2F")]
        public readonly MultiInputSlot<StructuredList> Lists = new MultiInputSlot<StructuredList>();
    }
}