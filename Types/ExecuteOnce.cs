using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ExecuteOnce : Instance<ExecuteOnce>
    {
        [Output(Guid = "5D73EBE6-9AA0-471A-AE6B-3F5BFD5A0F9C")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public ExecuteOnce()
        {
            Output.UpdateAction = Update;
        }

        private int _count = 0;
        private void Update(EvaluationContext context)
        {
            if (_count++ >= 1)
                return;

            var commands = Command.GetCollectedTypedInputs();

            // do preparation if needed
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Value?.PrepareAction?.Invoke(context);
            }

            // execute commands
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].GetValue(context);
            }

            // cleanup after usage
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Value?.RestoreAction?.Invoke(context);
            }
        }

        [Input(Guid = "7450033D-5797-40C9-B6C4-B6E8D27FE501")]
        public readonly MultiInputSlot<Command> Command = new MultiInputSlot<Command>();
    }
}