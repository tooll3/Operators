using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class SceneGroup : Instance<SceneGroup>
    {
        [Output(Guid = "E81C99CE-FCEE-4E7C-A1C7-0AA3B352B7E1")]
        public readonly Slot<Scene> Output = new Slot<Scene>();

        public SceneGroup()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            foreach (var input in Scene.GetCollectedTypedInputs())
            {
                input.GetValue(context);
            }
        }

        [Input(Guid = "5D73EBE6-9AA0-471A-AE6B-3F5BFD5A0F9C")]
        public readonly MultiInputSlot<Scene> Scene = new MultiInputSlot<Scene>();
    }
}