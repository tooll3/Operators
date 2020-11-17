using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_7d302c05_9898_4c56_a894_1f8f44b9b920
{
    public class PointsFromMeshData : Instance<PointsFromMeshData>
    {
        [Output(Guid = "c1038e01-cfa8-4baf-adf2-9fa39a066a03")]
        public readonly Slot<T3.Core.Command> Command = new Slot<T3.Core.Command>();

        [Output(Guid = "b5907b75-97f7-484a-8bb1-5e81a0fd114d")]
        public readonly Slot<T3.Core.DataTypes.BufferWithViews> Points = new Slot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "1e98e464-3dea-4eb3-9a2d-cff5d7873e71")]
        public readonly InputSlot<T3.Core.DataTypes.ParticleSystem> ParticleSystem = new InputSlot<T3.Core.DataTypes.ParticleSystem>();

        [Input(Guid = "b3dd7eb1-6ae6-460e-8851-630801012884")]
        public readonly InputSlot<int> EmitterId = new InputSlot<int>();

        [Input(Guid = "a18dd6ea-5708-4600-818d-d3bf7a3830b4")]
        public readonly InputSlot<int> EmitRate = new InputSlot<int>();

        [Input(Guid = "693d68d9-ef53-4dcb-a8ca-43971499f614")]
        public readonly InputSlot<bool> Emit = new InputSlot<bool>();

        [Input(Guid = "8e2fcfba-e391-4575-a048-ea5595c1bcb1")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "ae7036f7-d874-43e5-ae94-064304562487")]
        public readonly InputSlot<float> LifeTime = new InputSlot<float>();

        [Input(Guid = "41667677-3bc9-4d55-82ab-7b6cc2bfa077")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "491434a9-efe9-421a-8a5d-74aca3ebdb55")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "4f094bdf-0713-4765-9857-3ff63be170d0")]
        public readonly InputSlot<SharpDX.Direct3D11.Buffer> buffer = new InputSlot<SharpDX.Direct3D11.Buffer>();

        [Input(Guid = "1f4184f4-c186-43f4-9c01-e7af9c2e4920")]
        public readonly InputSlot<SharpDX.Direct3D11.ShaderResourceView> Data = new InputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "0f0652a0-8f5d-4f5c-ba1a-5bd3bd9a8f44")]
        public readonly InputSlot<int> Count = new InputSlot<int>();

        [Input(Guid = "e62d9cfd-7dee-48ff-b1de-6e2c5cb3a31a")]
        public readonly InputSlot<float> Seed = new InputSlot<float>();
    }
}

