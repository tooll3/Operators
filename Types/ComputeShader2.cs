using T3.Core;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class ComputeShader2 : Instance<ComputeShader2>
    {
        [Output(Guid = "f909b0bf-d825-4bdc-bcb8-5f90f382e199")]
        public readonly Slot<SharpDX.Direct3D11.ComputeShader2> ComputerShader = new Slot<SharpDX.Direct3D11.ComputeShader2>();

        private uint _computeShaderResId;
        public ComputeShader2()
        {
            ComputerShader.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var resourceManager = ResourceManager.Instance();

            if (Source.DirtyFlag.IsDirty || EntryPoint.DirtyFlag.IsDirty || DebugName.DirtyFlag.IsDirty)
            {
                string sourcePath = Source.GetValue(context);
                string entryPoint = EntryPoint.GetValue(context);
                string debugName = DebugName.GetValue(context);
                _computeShaderResId = resourceManager.CreateComputeShader2FromFile(sourcePath, entryPoint, debugName,
                                                                                  () => ComputerShader.DirtyFlag.Invalidate());
            }
            else
            {
                resourceManager.UpdateComputeShader2FromFile(Source.Value, _computeShaderResId, ref ComputerShader.Value);
            }

            if (_computeShaderResId != ResourceManager.NULL_RESOURCE)
            {
                ComputerShader.Value = resourceManager.GetComputeShader2(_computeShaderResId);
            }
        }

        [Input(Guid = "80ea12e1-948f-467e-af42-25c07b3b3bfc")]
        public readonly InputSlot<string> Source = new InputSlot<string>();

        [Input(Guid = "2615a9f0-dc2f-4663-b562-131bedbbf1a9")]
        public readonly InputSlot<string> EntryPoint = new InputSlot<string>();

        [Input(Guid = "4f7bc9ec-8899-4a60-9c67-081b3137ab62")]
        public readonly InputSlot<string> DebugName = new InputSlot<string>();
    }
}