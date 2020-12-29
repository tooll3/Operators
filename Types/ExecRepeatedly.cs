using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_46420979_1e56_4de3_a6ca_0447be1b9813
{
    public class ExecRepeatedly : Instance<ExecRepeatedly>
    {
        [Output(Guid = "5008c453-89ae-456b-9468-917abcb0af2e")]
        public readonly Slot<Command> Output = new Slot<Command>();

        public ExecRepeatedly()
        {
            Output.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var repeatCount = RepeatCount.GetValue(context).Clamp(0, 100);
            if (repeatCount <= 0)
                return;

            var commands = Command.CollectedInputs;

            // do preparation if needed
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Value?.PrepareAction?.Invoke(context);
            }
            
            // execute commands
            for (int repeation = 0; repeation < repeatCount; repeation++)
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    commands[i].GetValue(context);
                }
            }

            // cleanup after usage
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Value?.RestoreAction?.Invoke(context);
            }

            Command.DirtyFlag.Clear();
        }

        [Input(Guid = "d9de54b8-6d05-4cad-a1eb-bfa770a4520d")]
        public readonly MultiInputSlot<Command> Command = new MultiInputSlot<Command>();

        [Input(Guid = "FB4C2356-5FA9-4BEB-A909-805323D5F7C1")]
        public readonly InputSlot<int> RepeatCount = new InputSlot<int>();
    }
}