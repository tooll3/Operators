using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ca4fe8c4_cf61_4196_84e4_d69dc8869a25
{
    public class GetPbrParameters : Instance<GetPbrParameters>
    {
        [Output(Guid = "3D2EBD10-2670-46B7-8F1A-9475A81A516D", DirtyFlagTrigger = DirtyFlagTrigger.Animated)]
        public readonly Slot<Buffer> PbrParameterBuffer = new Slot<Buffer>();

        [Output(Guid = "D17FEEDB-CFDB-4B84-A399-45DA88239B61")]
        public readonly Slot<Texture2D> AlbedoColorMap = new Slot<Texture2D>();

        [Output(Guid = "C6CD0808-1D86-4BE6-AE62-6E14EB9FC098")]
        public readonly Slot<Texture2D> EmissiveColorMap = new Slot<Texture2D>();

        [Output(Guid = "FC442DD7-9E70-4D16-BDBE-68C9DCDC2EA9")]
        public readonly Slot<Texture2D> RoughnessSpecularMetallicOcclusionMap = new Slot<Texture2D>();

        [Output(Guid = "9F2A2455-FBB6-42B6-AACC-B85229C674D1")]
        public readonly Slot<Texture2D> NormalMap = new Slot<Texture2D>();

        public GetPbrParameters()
        {
            PbrParameterBuffer.UpdateAction = Update;
            AlbedoColorMap.UpdateAction = Update;
            EmissiveColorMap.UpdateAction = Update;
            RoughnessSpecularMetallicOcclusionMap.UpdateAction = Update;
            NormalMap.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            PbrParameterBuffer.Value = context.PbrMaterialParams;
            AlbedoColorMap.Value = context.PbrMaterialTextures.AlbedoColorMap;
            EmissiveColorMap.Value = context.PbrMaterialTextures.EmissiveColorMap;
            RoughnessSpecularMetallicOcclusionMap.Value = context.PbrMaterialTextures.RoughnessSpecularMetallicOcclusionMap;
            NormalMap.Value = context.PbrMaterialTextures.NormalMap;
        }
    }
}