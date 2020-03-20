using T3.Core.Operator.Helper;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_ad28819d_be62_4ed7_932a_fc861562983d
{
    public class TypoGrid : Instance<TypoGrid>
    {
        [Output(Guid = "982bd425-e781-42ad-9c58-f026fb6f193c")]
        public readonly Slot<Command> Output = new Slot<Command>();

        [Input(Guid = "7f86c4ea-48d1-4d3a-9a5a-cfbca0da4daa")]
        public readonly InputSlot<System.Numerics.Vector4> Color = new InputSlot<System.Numerics.Vector4>();

        [Input(Guid = "b49b85c9-9cc8-4118-a145-3d514108d3e2")]
        public readonly InputSlot<System.Numerics.Vector3> DisplaceAmount = new InputSlot<System.Numerics.Vector3>();

        [Input(Guid = "70cf8cfb-56a3-47bf-9dbc-fd1b5a2f7e82")]
        public readonly InputSlot<float> Size = new InputSlot<float>();

        [Input(Guid = "8d647ff3-7ae3-45c1-8ef5-c51c52a7b3c0")]
        public readonly InputSlot<float> Scale = new InputSlot<float>();

        [Input(Guid = "1178c7c0-12ec-4284-9d28-c357f8e8a8ca")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> DisplaceTexture = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "c50e7ae6-bdf1-4992-80c6-ae2f947a8a3f")]
        public readonly InputSlot<SharpDX.Size2> BufferSize = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "3ab49a26-3168-486a-be7e-35883659e0ef")]
        public readonly InputSlot<System.Numerics.Vector2> CellSize = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "d58039a7-12f2-46fa-b778-bfbb69027691")]
        public readonly InputSlot<System.Numerics.Vector2> CellPadding = new InputSlot<System.Numerics.Vector2>();

        [Input(Guid = "7c3dbe80-da67-430b-9aaa-f2e2f34fed73")]
        public readonly InputSlot<string> Text = new InputSlot<string>();

        [Input(Guid = "fc0074cd-3e09-4978-95f7-9ae8780539d3")]
        public readonly InputSlot<int> TextCycle = new InputSlot<int>();

        [Input(Guid = "94312d3c-ac74-4965-9874-ddd96d1437ff")]
        public readonly InputSlot<bool> WrapText = new InputSlot<bool>();
    }
}

