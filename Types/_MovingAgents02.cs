using SharpDX.Direct3D11;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_4edc34ed_36f4_4f24_837f_4cc5696b2baa
{
    public class _MovingAgents02 : Instance<_MovingAgents02>
    {
        [Output(Guid = "fc65f025-f050-403d-9fd9-097a7cc676ca")]
        public readonly Slot<Texture2D> ImgOutput = new Slot<Texture2D>();

        [Input(Guid = "b980985d-8d8c-4e32-bb5d-b4c9a1818b76")]
        public readonly InputSlot<float> Spin = new InputSlot<float>();

        [Input(Guid = "189e11e6-118f-4f77-82e0-6160f2340617")]
        public readonly InputSlot<float> TrailEngery = new InputSlot<float>();

        [Input(Guid = "d0e707a0-0bcf-456c-935b-9601e2c13243")]
        public readonly InputSlot<float> SampleDistance = new InputSlot<float>();

        [Input(Guid = "f97da962-23fe-4199-9c9f-ead73b9f2946")]
        public readonly InputSlot<float> SampleAngle = new InputSlot<float>();

        [Input(Guid = "7fd05362-920f-427c-b050-a487835a3ccd")]
        public readonly InputSlot<float> MoveDistance = new InputSlot<float>();

        [Input(Guid = "368ff5c2-26f3-4d97-b42f-0f72619f776d")]
        public readonly InputSlot<float> RotateAngle = new InputSlot<float>();

        [Input(Guid = "4e6f4f2d-b8cf-434e-8728-c60d00770ae7")]
        public readonly InputSlot<float> ReferenceEnergy = new InputSlot<float>();

        [Input(Guid = "2d68e9d6-5f54-4ee9-a9c5-aa3b60813329")]
        public readonly InputSlot<float> DecayRatio = new InputSlot<float>();

        [Input(Guid = "f333dbce-cc54-43ba-99b0-065426820f36")]
        public readonly InputSlot<float> RestoreLayout = new InputSlot<float>();

        [Input(Guid = "8ba49b49-8eee-461b-acd8-ba6bc21ba866")]
        public readonly InputSlot<bool> RestoreLayoutEnabled = new InputSlot<bool>();

        [Input(Guid = "3b2e07ad-8218-4740-9e3e-17949ed5fca6")]
        public readonly InputSlot<bool> ShowAgents = new InputSlot<bool>();

        [Input(Guid = "693957bc-5364-404d-84cd-0248fc609eca")]
        public readonly InputSlot<SharpDX.Size2> Resolution = new InputSlot<SharpDX.Size2>();

        [Input(Guid = "351eb848-829d-48cb-96c3-16c00366d34f")]
        public readonly InputSlot<int> AgentCount = new InputSlot<int>();

        [Input(Guid = "fec49356-96f9-4f11-9953-707758dcd353")]
        public readonly InputSlot<float> SnapToAnglesCount = new InputSlot<float>();

        [Input(Guid = "55c1bea6-6148-4423-a2e3-7fea9c773ec2")]
        public readonly InputSlot<float> SnapToAnglesAmount = new InputSlot<float>();

        [Input(Guid = "2b19f72d-db37-4e02-a780-2bb82adb1b54")]
        public readonly InputSlot<int> ComputeSteps = new InputSlot<int>();

        [Input(Guid = "ca393e2a-0244-4fc4-9d94-d67243d22024")]
        public readonly InputSlot<float> BRatio = new InputSlot<float>();

        [Input(Guid = "b66a05da-bd61-4eee-8805-5d9ae913ba91")]
        public readonly InputSlot<float> BTrail = new InputSlot<float>();

        [Input(Guid = "eaa722b1-3500-4cd5-ace7-69e62271e140")]
        public readonly InputSlot<float> BMoveDistance = new InputSlot<float>();

        [Input(Guid = "289c00c2-0718-4cd9-98bc-9d8303221760")]
        public readonly InputSlot<float> BRotate = new InputSlot<float>();


    }
}

