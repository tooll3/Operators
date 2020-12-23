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
            var totalElementCount = 0;
            var listsCollectedInputs = Lists.CollectedInputs.Select(c => c.GetValue(context));
            
            foreach (var xxx in listsCollectedInputs)
            {
                Log.Debug("Found " + xxx);
                //totalElementCount+= xxx.Elements.count; //<--- doesn't work of course
            }
            // var pointArray = Lists.GetValue(context);
            // if (pointArray == null || pointArray.Length == 0)
            // {
            //     Length.Value = 0;
            //     Log.Warning("Invalid input for PointsToBuffer");
            //     return;
            // }
            //
            // Length.Value = pointArray.Length;
            //
            // var resourceManager = ResourceManager.Instance();
            // if (_bufferData.Length != pointArray.Length)
            // {
            //     _bufferData = new T3.Core.DataTypes.Point[pointArray.Length];
            // }
            //
            //
            // for (int index = 0; index < pointArray.Length; index++)
            // {
            //     _bufferData[index] = pointArray[index];
            // }
            //
            // var stride = 32;
            //
            // _bufferWithViews.Buffer = _buffer;
            // resourceManager.SetupStructuredBuffer(_bufferData, stride * pointArray.Length, stride, ref _buffer);
            // resourceManager.CreateStructuredBufferSrv(_buffer, ref _bufferWithViews.Srv);
            // resourceManager.CreateStructuredBufferUav(_buffer, UnorderedAccessViewBufferFlags.None, ref _bufferWithViews.Uav);
            // OutBuffer.Value = _bufferWithViews;
        }

        private Buffer _buffer;
        private T3.Core.DataTypes.Point[] _bufferData = new T3.Core.DataTypes.Point[0];
        private BufferWithViews _bufferWithViews = new BufferWithViews();

        [Input(Guid = "08F181BB-9777-497C-871D-BCC1FF252F2F")]
        public readonly MultiInputSlot<StructuredList> Lists = new MultiInputSlot<StructuredList>();
    }
}