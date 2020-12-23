using System;
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

namespace T3.Operators.Types.Id_924b8cc0_5b4b_41d0_a71b_b26465683910
{
    public class _SpeciesDefinition : Instance<_SpeciesDefinition>
    {

        // //
        // [Output(Guid = "0A3AE6BF-B720-4CF6-B683-65D9BFADB777")]
        // public readonly Slot<Buffer> OutBuffer = new Slot<Buffer>();

        [Output(Guid = "8e67675a-ab49-46c5-a2fb-a7eb6d808d14")]
        public readonly Slot<BufferWithViews> OutBuffer = new Slot<BufferWithViews>();

        
        [Output(Guid = "848b8d3e-4bed-4ccf-b591-26ef72dcafd5")]
        public readonly Slot<int> Length = new Slot<int>();
        
        public _SpeciesDefinition()
        {

            Length.UpdateAction = Update;
            OutBuffer.UpdateAction = Update;
            // OutBuffer.UpdateAction = Update;
        }

        // [StructLayout(LayoutKind.Explicit, Size = 16)]
        // struct BufferEntry
        // {
        //     [FieldOffset(0)]
        //     public SharpDX.Vector4 Point;
        // }


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
                _bufferData = new T3.Core.DataTypes.Point[pointArray.Length];
            }
            

            for (int index = 0; index < pointArray.Length; index++)
            {
                _bufferData[index] = pointArray[index];
            }

            var stride = 32;

            _bufferWithViews.Buffer = _buffer;
            resourceManager.SetupStructuredBuffer(_bufferData, stride * pointArray.Length, stride, ref _buffer);
            resourceManager.CreateStructuredBufferSrv(_buffer, ref _bufferWithViews.Srv);
            resourceManager.CreateStructuredBufferUav(_buffer, UnorderedAccessViewBufferFlags.None, ref _bufferWithViews.Uav);
            OutBuffer.Value = _bufferWithViews;
        }

        private Buffer _buffer;
        private T3.Core.DataTypes.Point[] _bufferData = new T3.Core.DataTypes.Point[0];
        private BufferWithViews _bufferWithViews = new BufferWithViews();

        [Input(Guid = "5a40e2de-c940-4573-9fd4-3799de124a2e")]
        public readonly InputSlot<T3.Core.DataTypes.Point[]> PointArray = new InputSlot<T3.Core.DataTypes.Point[]>();
    }
}