using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_4f8c54a3_d146_4f7c_8628_17697b0201c9
{
    public class _SimulateBoids : Instance<_SimulateBoids>
    {
        [Output(Guid = "5f95c6f4-e654-4858-90fe-5dfa540e2186")]
        public readonly Slot<Texture2D> ImgOutput = new Slot<Texture2D>();

        [Input(Guid = "60edbf47-c594-4079-89cb-abffc2a5baa0")]
        public readonly InputSlot<int> AgentCount = new InputSlot<int>();

        [Input(Guid = "ff301ad3-74c9-4bef-88c8-ab883c76b67a")]
        public readonly InputSlot<SharpDX.Size2> BlockCount = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "eb1f8e21-521a-41f4-a761-1c47680b6afc")]
        public readonly InputSlot<T3.Core.DataTypes.BufferWithViews> BreedsBuffer = new InputSlot<T3.Core.DataTypes.BufferWithViews>();

        [Input(Guid = "2d740120-1fe5-444f-80a0-2201c4a9bfcd")]
        public readonly InputSlot<int> ComputeSteps = new InputSlot<int>();

        [Input(Guid = "df993647-f384-4371-8196-2f52ba9e2d5a")]
        public readonly InputSlot<bool> RestoreLayoutEnabled = new InputSlot<bool>();

        [Input(Guid = "cf868ea8-6a73-4c93-b435-b4e3a1a95167")]
        public readonly InputSlot<float> RestoreLayout = new InputSlot<float>();

        [Input(Guid = "01b6ac2b-be17-467c-bb64-69dc04194134")]
        public readonly InputSlot<float> EffectLayer = new InputSlot<float>();


    }
}

