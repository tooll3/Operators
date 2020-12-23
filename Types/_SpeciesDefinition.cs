using System.Numerics;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using T3.Core;
//using SharpDX;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;


namespace T3.Operators.Types.Id_924b8cc0_5b4b_41d0_a71b_b26465683910
{
    using BreedList = StructuredList<_SpeciesDefinition.Breed>;

    public class _SpeciesDefinition : Instance<_SpeciesDefinition>
    {
        [Output(Guid = "55498833-FF69-489F-AFE6-D54150920C56")]
        public readonly Slot<StructuredList> OutBuffer = new Slot<StructuredList>();
        
        public _SpeciesDefinition()
        {
            OutBuffer.UpdateAction = Update;
        }
        
        private void Update(EvaluationContext context)
        {
            _breeds._typedElements[0].ComfortZones = ComfortZones.GetValue(context);
            _breeds._typedElements[0].BaseRotation = BaseRotation.GetValue(context);
            OutBuffer.Value = _breeds;
        }
        
        [StructLayout(LayoutKind.Explicit, Size = 16 * 4)]
        public struct Breed
        {
            [FieldOffset(0 * 4)]
            public Vector4 ComfortZones;
        
            [FieldOffset(4 * 4)]
            public Vector4 Emit;
        
            [FieldOffset(8 * 4)]
            public float SideAngle;
        
            [FieldOffset(9 * 4)]
            public float SideRadius;
        
            [FieldOffset(10 * 4)]
            public float FrontRadius;
        
            [FieldOffset(11 * 4)]
            public float BaseMovement;
        
            [FieldOffset(12 * 4)]
            public float BaseRotation;
        
            [FieldOffset(13 * 4)]
            public float MoveToComfort;
        
            [FieldOffset(14 * 4)]
            public float RotateToComfort;
        
            [FieldOffset(15 * 4)]
            public float _padding;
        }
        
        // public class BreedList : StructuredList
        // {
        //     public BreedList(int count) : base(typeof(Breed))
        //     {
        //         _typedElements = new Breed[count];
        //     }
        //
        //     public Breed[] _typedElements { get; }
        //     public override object Elements => _typedElements;
        // }
        
        private BreedList _breeds = new BreedList(1);
        
        [Input(Guid = "8C4E188C-18AB-4F69-A60A-14A8E5A12F91")]
        public readonly InputSlot<System.Numerics.Vector4> ComfortZones = new InputSlot<System.Numerics.Vector4>();
        
        [Input(Guid = "3B743067-B241-4520-8DA8-56398C76448F")]
        public readonly InputSlot<float> BaseRotation = new InputSlot<float>();
    }
}