using System;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace T3.Operators.Types.Id_58351c8f_4a73_448e_b7bb_69412e71bd76
{
    public class ExecuteSRVUpdate : Instance<ExecuteSRVUpdate>
    {
        [Output(Guid = "FA194B15-E208-4742-9BD2-41670C3D6D32")]
        public readonly Slot<ShaderResourceView> Output = new Slot<ShaderResourceView>();

        public ExecuteSRVUpdate()
        {
            Output.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            // This will execute the input
            UpdateCommand.GetValue(context);
            
            Output.Value = ShaderResourceView.GetValue(context);;
        }

        [Input(Guid = "51110D89-083E-42B8-B566-87B144DFBED9")]
        public readonly InputSlot<Command> UpdateCommand = new InputSlot<Command>();
        
        [Input(Guid = "60BCBF27-4F06-4DA4-A87D-5C7115C0AD8A")]
        public readonly InputSlot<ShaderResourceView> ShaderResourceView = new InputSlot<ShaderResourceView>();
    }
}