using SharpDX;
using T3.Core.Operator;

namespace T3.Operators.Types
{
    public class GetTextureSize : Instance<GetTextureSize>
    {
        [Output(Guid = "be16d5d3-4d21-4d5a-9e4c-c7b2779b6bdc")]
        public readonly Slot<SharpDX.Size2> Size = new Slot<SharpDX.Size2>();

        
        public GetTextureSize()
        {
            Size.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            Size.Value = FallbackSize.GetValue(context);

            var texture = Texture.GetValue(context);
            if (texture != null)
            {
                Size.Value = new Size2(texture.Description.Width, texture.Description.Height);
            }
            else
            {
                Size.Value = FallbackSize.GetValue(context);
            }
        }

        [Input(Guid = "8b15d8e1-10c7-41e1-84db-a85e31e0c909")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Texture = new InputSlot<SharpDX.Direct3D11.Texture2D>();
        
        [Input(Guid = "52b2f067-5619-4d8d-a982-58668a8dc6a4")]
        public readonly InputSlot<SharpDX.Size2> FallbackSize = new InputSlot<SharpDX.Size2>();
    }
}
