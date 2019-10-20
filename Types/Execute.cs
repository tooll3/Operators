using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class Execute : Instance<Execute>
    {
        [Output(Guid = "E81C99CE-FCEE-4E7C-A1C7-0AA3B352B7E1")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public Execute()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
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

        [Input(Guid = "5D73EBE6-9AA0-471A-AE6B-3F5BFD5A0F9C")]
        public readonly MultiInputSlot<Command> Command = new MultiInputSlot<Command>();
    }
}