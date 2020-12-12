using System.Numerics;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_2b6981f8_f66c_4132_9f37_6536d477ed65
{
    public class CASim : Instance<CASim>
    {
        [Output(Guid = "b55532fe-9582-46cf-b56e-d699b5ecd9d0")]
        public readonly Slot<Texture2D> Output = new Slot<Texture2D>();

        [Input(Guid = "419b5101-26ff-4c56-9e9e-09258c5870b2")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture2d = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "3c9a01bd-7afb-425f-8f05-92c20261a507")]
        public readonly InputSlot<float> Threshold = new InputSlot<float>();

        [Input(Guid = "ce599a30-6b77-4e6b-b0ab-7ba7b1b91667")]
        public readonly InputSlot<float> MaxSteps = new InputSlot<float>();

        [Input(Guid = "1a09496a-0cca-4f87-8b5e-28d5b183e6e1")]
        public readonly InputSlot<float> Range = new InputSlot<float>();

        [Input(Guid = "ef8b0c8d-79b5-4fd5-a676-79522f957a52")]
        public readonly InputSlot<float> RandomAmount = new InputSlot<float>();

        [Input(Guid = "5eb23b44-3416-4ba2-94b2-d3f1abae7feb")]
        public readonly InputSlot<bool> AddNoise2 = new InputSlot<bool>();

        [Input(Guid = "bf17d586-a38c-403f-8d82-3e11255b0b66")]
        public readonly InputSlot<bool> FullSpeed = new InputSlot<bool>();

        [Input(Guid = "69c7792c-6075-4b15-97de-d7ec6b45bc45")]
        public readonly InputSlot<float> R_xThreshold = new InputSlot<float>();

        [Input(Guid = "1e9bf3f4-a08e-4588-b4c1-9dc2b63e2cb6")]
        public readonly InputSlot<float> G_xStates = new InputSlot<float>();

        [Input(Guid = "252b71ed-4c62-4229-ae1d-99697e604088")]
        public readonly InputSlot<bool> UseMooreRegion = new InputSlot<bool>();

        [Input(Guid = "06d37a69-f635-4c81-b0e4-13782aaf57d5")]
        public readonly InputSlot<float> Scale = new InputSlot<float>();

        [Input(Guid = "d1fe6b7e-0fd4-4ec3-8e61-a36a1311f111")]
        public readonly InputSlot<float> Rotate = new InputSlot<float>();

        [Input(Guid = "2aee77a4-b2d4-44c1-8e16-8d155456cacb")]
        public readonly MultiInputSlot<T3.Core.Command> AdditionalGemeotry = new MultiInputSlot<T3.Core.Command>();

    }
}