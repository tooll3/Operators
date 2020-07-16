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

            var uav1 = BufferUav.GetValue(context);
            if (uav1 == null)
            {
                return;
            }
            
            var uav2 = BufferUav2.GetValue(context);
            if (uav2 == null)
            {
                return;
            }

            var srv1 = BufferSrv.GetValue(context);
            var srv2 = BufferSrv2.GetValue(context);
            
            var resourceManager = ResourceManager.Instance();
            var device = resourceManager.Device;
            var deviceContext = device.ImmediateContext;
            var csStage = deviceContext.ComputeShader;

            var prevShader = csStage.Get();
            var prevConstBuffer = csStage.GetConstantBuffers(0, 1)[0];
            ComputeShader sortShader = resourceManager.GetComputeShader(_sortShaderId);
            ComputeShader transposeShader = resourceManager.GetComputeShader(_transposeShaderId);
            csStage.Set(sortShader);
            csStage.SetConstantBuffer(0, _parameterConstBuffer);
            csStage.SetUnorderedAccessView(0, uav1);

            const int numBufferElements = 512 * 512 * 2;
            const int bitonicBlockSize = 1024;
            for (int level = 2; level <= bitonicBlockSize; level <<= 1)
            {
                Int4 sortParams = new Int4(level, level, bitonicBlockSize, numBufferElements / bitonicBlockSize);
                resourceManager.SetupConstBuffer(sortParams, ref _parameterConstBuffer);
                deviceContext.Dispatch(numBufferElements / bitonicBlockSize, 1, 1);
            }

            const int matWidth = bitonicBlockSize;
            const int matHeight = numBufferElements / bitonicBlockSize;
            // Then sort the rows and columns for the levels > than the block size
            // Transpose. Sort the Columns. Transpose. Sort the Rows.
            for (int level = (bitonicBlockSize * 2); level <= numBufferElements; level <<= 1)
            {
                // Transpose the data from buffer 1 into buffer 2
                // SetConstants(shader, (level / BITONIC_BLOCK_SIZE), (level & ~NUM_ELEMENTS) / BITONIC_BLOCK_SIZE, MATRIX_WIDTH, MATRIX_HEIGHT);
                // shader.SetBuffer(kTranspose, "Input", inBuffer);
                // shader.SetBuffer(kTranspose, "Data", tmpBuffer);
                // shader.Dispatch(kTranspose, (int)(MATRIX_WIDTH / TRANSPOSE_BLOCK_SIZE), (int)(MATRIX_HEIGHT / TRANSPOSE_BLOCK_SIZE), 1);
                csStage.Set(transposeShader);
                csStage.SetShaderResource(0, null);
                csStage.SetUnorderedAccessView(0, null);
                csStage.SetShaderResource(0, srv1);
                csStage.SetUnorderedAccessView(0, uav2);
                Int4 param = new Int4(level / bitonicBlockSize, (level & ~numBufferElements) / bitonicBlockSize, matWidth, matHeight);
                resourceManager.SetupConstBuffer(param, ref _parameterConstBuffer);
                deviceContext.Dispatch(matWidth / 32, matHeight / 32, 1);

                // shader.SetBuffer(kSort, "Data", tmpBuffer);
                // shader.Dispatch(kSort, (int)(NUM_ELEMENTS / BITONIC_BLOCK_SIZE), 1, 1);
                csStage.Set(sortShader);
                deviceContext.Dispatch(numBufferElements / bitonicBlockSize, 1, 1);

                // Transpose the data from buffer 2 back into buffer 1
                // SetConstants(shader, BITONIC_BLOCK_SIZE, level, MATRIX_HEIGHT, MATRIX_WIDTH);
                // shader.SetBuffer(kTranspose, "Input", tmpBuffer);
                // shader.SetBuffer(kTranspose, "Data", inBuffer);
                // shader.Dispatch(kTranspose, (int)(MATRIX_HEIGHT / TRANSPOSE_BLOCK_SIZE), (int)(MATRIX_WIDTH / TRANSPOSE_BLOCK_SIZE), 1);
                csStage.Set(transposeShader);
                csStage.SetShaderResource(0, null);
                csStage.SetUnorderedAccessView(0, null);
                csStage.SetShaderResource(0, srv2);
                csStage.SetUnorderedAccessView(0, uav1);
                param = new Int4(bitonicBlockSize, level, matHeight, matWidth);
                resourceManager.SetupConstBuffer(param, ref _parameterConstBuffer);
                deviceContext.Dispatch(matHeight / 32, matWidth / 32, 1);

                // shader.SetBuffer(kSort, "Data", inBuffer);
                // shader.Dispatch(kSort, (int)(NUM_ELEMENTS / BITONIC_BLOCK_SIZE), 1, 1);
                csStage.Set(sortShader);
                deviceContext.Dispatch(numBufferElements / bitonicBlockSize, 1, 1);
            }

            csStage.SetConstantBuffer(0, prevConstBuffer);
            csStage.Set(prevShader);
        }

        public void Init()
        {
            var resourceManager = ResourceManager.Instance();

            if (_sortShaderId == ResourceManager.NullResource)
            {
                string sourcePath = @"Resources\proj-partial\particle\bitonic-sort.hlsl";
                string entryPoint = "bitonicSort";
                string debugName = "bitonic-sort";
                _sortShaderId = resourceManager.CreateComputeShaderFromFile(sourcePath, entryPoint, debugName, null);
            }

            if (_transposeShaderId == ResourceManager.NullResource)
            {
                string sourcePath = @"Resources\proj-partial\particle\bitonic-transpose.hlsl";
                string entryPoint = "transpose";
                string debugName = "bitonic-transpose";
                _transposeShaderId = resourceManager.CreateComputeShaderFromFile(sourcePath, entryPoint, debugName, null);
            }

            InitConstBuffer();
        }

        private void InitConstBuffer()
        {
            ResourceManager.Instance().SetupConstBuffer(Int4.Zero, ref _parameterConstBuffer);
            _parameterConstBuffer.DebugName = "GpuSort-ParameterConstBuffer";
        }

        private Buffer _parameterConstBuffer;
        private uint _sortShaderId;
        private uint _transposeShaderId;

        [Input(Guid = "37dddd93-2b54-4598-aaca-40710ed06417")]
        public readonly InputSlot<SharpDX.Direct3D11.UnorderedAccessView> BufferUav = new InputSlot<SharpDX.Direct3D11.UnorderedAccessView>();

        [Input(Guid = "79d7bbd1-37a3-49eb-b705-e39345b50568")]
        public readonly InputSlot<SharpDX.Direct3D11.UnorderedAccessView> BufferUav2 = new InputSlot<SharpDX.Direct3D11.UnorderedAccessView>();

        [Input(Guid = "187b350b-71da-4cad-9e44-6f536e647e97")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> BufferSrv = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "5dfdc602-00c9-4125-b49d-ca15c769f43e")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> BufferSrv2 = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();
    }
}