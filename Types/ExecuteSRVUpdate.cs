using System;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_58351c8f_4a73_448e_b7bb_69412e71bd76
{
    public class ExecuteSRVUpdate : Instance<ExecuteSRVUpdate>
    {
        [Output(Guid = "9A66687E-A834-452C-A652-BA1FC70C2C7B")]
        public readonly Slot<BufferWithViews> Output2 = new Slot<BufferWithViews>();

        
        public ExecuteSRVUpdate()
        {
            Output2.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            // This will execute the input
            UpdateCommand.GetValue(context);
            
            Output2.Value = BufferWithViews.GetValue(context);
        }

        [Input(Guid = "51110D89-083E-42B8-B566-87B144DFBED9")]
        public readonly InputSlot<Command> UpdateCommand = new InputSlot<Command>();
        
        [Input(Guid = "72CFE742-88FB-41CD-B6CF-D96730B24B23")]
        public readonly InputSlot<BufferWithViews> BufferWithViews = new InputSlot<BufferWithViews>();
    }
}