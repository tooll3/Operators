using System;
using System.Runtime.InteropServices;
using SharpDX;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_59b810f1_7849_40a7_ae10_7e8008685311
{
    public class PointsToBuffer : Instance<PointsToBuffer>
    {
        [Output(Guid = "00027a91-db2f-4eed-a340-3cdf692be853")]
        public readonly Slot<SharpDX.Direct3D11.ShaderResourceView> PointBufferSrv = new Slot<SharpDX.Direct3D11.ShaderResourceView>();
        // //
        // [Output(Guid = "0A3AE6BF-B720-4CF6-B683-65D9BFADB777")]
        // public readonly Slot<Buffer> OutBuffer = new Slot<Buffer>();

        
        [Output(Guid = "36FD3A40-6416-4BCB-9FAC-9CD9221BEBA8")]
        public readonly Slot<int> Length = new Slot<int>();
        
        public PointsToBuffer()
        {
            PointBufferSrv.UpdateAction = Update;
            Length.UpdateAction = Update;
            // OutBuffer.UpdateAction = Update;
        }

        [StructLayout(LayoutKind.Explicit, Size = 16)]
        struct BufferEntry
        {
            [FieldOffset(0)]
            public SharpDX.Vector4 Point;
        }


        private void Update(EvaluationContext context)
        {
            var pointArray = PointArray.GetValue(context);
            if (pointArray == null || pointArray.Length == 0)
            {
                Length.Value = 0;
                Log.Warning("Invalid input for PointsToBuffer");
                return;
            }

            Length.Value = pointArray.Length;
            
            var resourceManager = ResourceManager.Instance();
            if (_bufferData.Length != pointArray.Length)
            {
                _bufferData = new BufferEntry[pointArray.Length];
            }
            

            for (int index = 0; index < pointArray.Length; index++)
            {
                _bufferData[index].Point = pointArray[index];
            }

            var stride = 16;
            resourceManager.SetupStructuredBuffer(_bufferData, stride * pointArray.Length, stride, ref _buffer);
            //resourceManager.SetupStructuredBuffer(pointArray, stride * pointArray.Length, stride, ref pointArray);
            resourceManager.CreateStructuredBufferSrv(_buffer, ref PointBufferSrv.Value);
            //PointBufferSrv.
        }

        private Buffer _buffer;
        private BufferEntry[] _bufferData = new BufferEntry[0];


        [Input(Guid = "6fddc26b-31e2-41f1-b86c-0b71d898801a")]
        public readonly InputSlot<SharpDX.Vector4[]> PointArray = new InputSlot<SharpDX.Vector4[]>();
    }
}