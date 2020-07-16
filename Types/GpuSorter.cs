using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_94a85a93_7d5c_401c_930c_c3a97a32932f
{
    public class GpuSorter : Instance<GpuSorter>
    {
        [Output(Guid = "14e52376-e375-495d-a466-74731457b189")]
        public readonly Slot<Command> Command = new Slot<Command>();

        public GpuSorter()
        {
            Command.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (_parameterConstBuffer == null)
            {
                Init();
            }

            var inputUav = BufferUav.GetValue(context);
            if (inputUav == null)
            {
                return;
            }

            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;
            var deviceContext = device.ImmediateContext;
            var csStage = deviceContext.ComputeShader;

            var prevShader = csStage.Get();
            var prevConstBuffer = csStage.GetConstantBuffers(0, 1)[0];
            ComputeShader sortShader = resourceManager.GetComputeShader(_sortShaderId);
            csStage.Set(sortShader);
            csStage.SetConstantBuffer(0, _parameterConstBuffer);
            csStage.SetUnorderedAccessView(0, inputUav);

            // Int3 dispatchCount = Dispatch.GetValue(context);
            // deviceContext.Dispatch(dispatchCount.X, dispatchCount.Y, dispatchCount.Z);
            deviceContext.Dispatch(1, 1, 1);

            csStage.SetConstantBuffer(0, prevConstBuffer);
            csStage.Set(prevShader);
        }

        public void Init()
        {
            if (_sortShaderId == ResourceManager.NullResource)
            {
                string sourcePath = @"Resources\proj-partial\particle\bitonic-sort.hlsl";
                string entryPoint = "bitonicSort";
                string debugName = "bitonic-sort";
                var resourceManager = ResourceManager.Instance();
                _sortShaderId = resourceManager.CreateComputeShaderFromFile(sourcePath, entryPoint, debugName, null);
            }

            InitConstBuffer();
        }

        private void InitConstBuffer()
        {
            ResourceManager.Instance().SetupConstBuffer(Vector4.Zero, ref _parameterConstBuffer);
            _parameterConstBuffer.DebugName = "GpuSort-ParameterConstBuffer";
        }

        private Buffer _parameterConstBuffer;
        private uint _sortShaderId;

        [Input(Guid = "37dddd93-2b54-4598-aaca-40710ed06417")]
        public readonly InputSlot<SharpDX.Direct3D11.UnorderedAccessView> BufferUav = new InputSlot<SharpDX.Direct3D11.UnorderedAccessView>();
    }
}