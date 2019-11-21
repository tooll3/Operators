using System.Linq;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using T3.Core;

namespace T3.Operators.Helpers
{
    public class ParticleSystem
    {
        public Buffer ParticleBuffer;
        public UnorderedAccessView ParticleBufferUav;
        public ShaderResourceView ParticleBufferSrv;

        public Buffer DeadParticleIndices;
        public UnorderedAccessView DeadParticleIndicesUav;

        public Buffer AliveParticleIndices;
        public UnorderedAccessView AliveParticleIndicesUav;
        public ShaderResourceView AliveParticleIndicesSrv;

        public Buffer IndirectArgBuffer;
        public UnorderedAccessView IndirectArgBufferUav;
        public ShaderResourceView IndirectArgBufferSrv;

        public Buffer ParticleCountConstBuffer;

        public int MaxCount { get; set; } = 20480;
        public readonly int ParticleSizeInBytes = 48;
        public int ParticleSystemSizeInBytes => MaxCount * ParticleSizeInBytes;

        public void Init()
        {
            InitParticleBufferAndViews();
            InitDeadParticleIndices();
            InitAliveParticleIndices();
            InitIndirectArgBuffer();
            InitParticleCountConstBuffer();
        }

        private void InitParticleBufferAndViews()
        {
            var resourceManager = ResourceManager.Instance();
            var bufferData = Enumerable.Repeat(-10.0f, ParticleSystemSizeInBytes/4).ToArray(); // init with negative lifetime other values doesn't matter
            resourceManager.SetupStructuredBuffer(bufferData, ref ParticleBuffer);
            resourceManager.CreateStructuredBufferUav(ParticleBuffer, UnorderedAccessViewBufferFlags.None, ref ParticleBufferUav);
            resourceManager.CreateStructuredBufferSrv(ParticleBuffer, ref ParticleBufferSrv);
        }

        private void InitDeadParticleIndices()
        {
            var resourceManager = ResourceManager.Instance();
            resourceManager.SetupStructuredBuffer(4*MaxCount, 4, ref DeadParticleIndices);
            resourceManager.CreateStructuredBufferUav(DeadParticleIndices, UnorderedAccessViewBufferFlags.Append, ref DeadParticleIndicesUav);
        }

        private void InitAliveParticleIndices()
        {
            var resourceManager = ResourceManager.Instance();
            resourceManager.SetupStructuredBuffer(4*MaxCount, 4, ref AliveParticleIndices);
            resourceManager.CreateStructuredBufferUav(AliveParticleIndices, UnorderedAccessViewBufferFlags.Counter, ref AliveParticleIndicesUav);
            resourceManager.CreateStructuredBufferSrv(AliveParticleIndices, ref AliveParticleIndicesSrv);
        }

        private void InitIndirectArgBuffer()
        {
            var resourceManager = ResourceManager.Instance();
            int sizeInBytes = 16;
            resourceManager.SetupIndirectBuffer(sizeInBytes, ref IndirectArgBuffer);
            resourceManager.CreateBufferUav<uint>(IndirectArgBuffer, Format.R32_UInt, ref IndirectArgBufferUav);
        }
        
        private void InitParticleCountConstBuffer()
        {
            ResourceManager.Instance().SetupConstBuffer(Vector4.Zero, ref ParticleCountConstBuffer);
            ParticleCountConstBuffer.DebugName = "ParticleCountConstBuffer";
        }
    }
}