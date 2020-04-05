using System;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_b5102fba_f05b_43fc_aa1d_613fe1d68ad2
{
    public class Grain : Instance<Grain>
    {
        [Output(Guid = "df388f27-f5b6-417b-87a7-a6a59b625128")]
        public readonly Slot<SharpDX.Direct3D11.Texture2D> TextureOutput = new Slot<SharpDX.Direct3D11.Texture2D>();


        [Input(Guid = "4525c76c-cdcf-47f3-aa96-335cfc5b5c1b")]
        public readonly InputSlot<SharpDX.Direct3D11.Texture2D> Image = new InputSlot<SharpDX.Direct3D11.Texture2D>();

        [Input(Guid = "195da7e0-5279-4900-80cd-5635e96ab454")]
        public readonly InputSlot<float> Amount = new InputSlot<float>();

        [Input(Guid = "903c0270-dc46-402e-8088-8db8368a6dfb")]
        public readonly InputSlot<float> Color = new InputSlot<float>();

        [Input(Guid = "d24cce46-dd3f-4047-a33f-50abbca89cc4")]
        public readonly InputSlot<float> Exponent = new InputSlot<float>();
    }
}

