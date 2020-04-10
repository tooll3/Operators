using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_557fcca1_5406_4594_9dd2_9122d3b3a1e2
{
    public class AddPointClound : Instance<AddPointClound>
    {
        [Output(Guid = "598a616c-5b49-4d6a-9b65-38879d78cc38")]
        public readonly Slot<T3.Core.Command> Command = new Slot<T3.Core.Command>();

        [Input(Guid = "54432dc7-83dd-4447-9bfc-b6c9654b5bbb")]
        public readonly InputSlot<string> ShaderFilename = new InputSlot<string>();

        [Input(Guid = "6385b7e3-414b-4c9f-add4-77d2dcb85849")]
        public readonly MultiInputSlot<SharpDX.Direct3D11.ShaderResourceView> ShaderResources = new MultiInputSlot<SharpDX.Direct3D11.ShaderResourceView>();

        [Input(Guid = "c138ad9e-5625-4ac1-b6f9-9fff7aba523e")]
        public readonly InputSlot<T3.Core.Operator.Helper.ParticleSystem> ParticleSystem = new InputSlot<T3.Core.Operator.Helper.ParticleSystem>();

        [Input(Guid = "4353c57e-b7f7-4e3d-a447-ed7c443ed187")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "7e1e6a29-d59a-42f4-b128-d377b1f34403")]
        public readonly InputSlot<System.Numerics.Vector4> ColorScatter = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "91d2ff32-1a54-47da-ac71-2c56d984a20a")]
        public readonly InputSlot<float> LifeTime = new InputSlot<float>();

        [Input(Guid = "fa3db491-3dcc-4565-b677-784f05d79b08")]
        public readonly InputSlot<float> ScatterPosition = new InputSlot<float>();
    }
}

