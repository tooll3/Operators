using T3.Core.DataTypes;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_c6911113_9411_4706_ad16_9e7bf58ad6c6
{
    public class RectTextureEmit : Instance<RectTextureEmit>
    {
        [Output(Guid = "4efe1aa1-fc4c-495d-a25d-bcffe6491611")]
        public readonly Slot<Command> Output = new Slot<Command>();


        [Input(Guid = "765b2330-777c-4b7d-bfa0-15f4701bedae")]
        public readonly InputSlot<ParticleSystem> ParticleSystem = new InputSlot<ParticleSystem>();

        [Input(Guid = "7d0e8a44-367c-4eb0-8fa3-d26a67a5ec35")]
        public readonly InputSlot<Texture2D> Texture = new InputSlot<Texture2D>();

        [Input(Guid = "d9e528ba-0aa1-42b2-8169-984d7a340228")]
        public readonly InputSlot<int> EmitCountPerFrame = new InputSlot<int>();

    }
}

