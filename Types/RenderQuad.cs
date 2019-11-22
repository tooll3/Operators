using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class RenderQuad : Instance<RenderQuad>
    {
        [Output(Guid = "1ff16183-13b9-4c8f-a82a-77e8be0c641b")]
        public readonly Slot<Command> Output = new Slot<Command>();


    }
}

