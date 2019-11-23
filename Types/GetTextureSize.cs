using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using T3.Core.Operator;
using Vector2 = System.Numerics.Vector2;

namespace T3.Operators.Types
{
    public class GetTextureSize : Instance<GetTextureSize>
    {
        // [Output(Guid = "1652BAC6-D496-4726-96F5-DB47927CCA8B")]
        // public readonly Slot<System.Numerics.Vector2> Size = new Slot<System.Numerics.Vector2>();
        
        [Output(Guid = "BE16D5D3-4D21-4D5A-9E4C-C7B2779B6BDC")]
        public readonly Slot<Size2> Size = new Slot<Size2>();

        
        public GetTextureSize()
        {
            Size.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            if (!Texture.DirtyFlag.IsDirty)
                return; // nothing to do
            
            var texture = Texture.GetValue(context);
            if (texture != null)
            {
                Size.Value = new Size2(
                                         texture.Description.Width,
                                         texture.Description.Height);
                return;
                
            }
            Size.Value = new  Size2(1,1);
        }

        // [Input(Guid = "10D5DEE1-A85C-4462-ABF4-401B0BCAF275")]
        // public readonly InputSlot<float> XXX = new InputSlot<float>();
        //
        // [Input(Guid = "6B635BF9-E6A4-42BD-AD6F-A4C50C56E921")]
        // public readonly InputSlot<ShaderResourceView> Srv = new InputSlot<ShaderResourceView>();

        [Input(Guid = "8B15D8E1-10C7-41E1-84DB-A85E31E0C909")]
        public readonly InputSlot<Texture2D> Texture = new InputSlot<Texture2D>();
    }
}
