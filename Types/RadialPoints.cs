using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Win32;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Vector4 = SharpDX.Vector4;

namespace T3.Operators.Types.Id_a38626d8_3145_4aa9_820f_ca16b3411985
{
    public class RadialPoints : Instance<RadialPoints>
    {
        [Output(Guid = "58c8870e-0931-4881-a923-3405d1be9a5e")]
        public readonly Slot<SharpDX.Vector4[]> Result = new Slot<SharpDX.Vector4[]>();

        public RadialPoints()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var count = Count.GetValue(context).Clamp(1, 10000);
            if (_points.Length != count)
                _points = new SharpDX.Vector4[count];

            var axis = Axis.GetValue(context);
            var center = Center.GetValue(context);
            var offset = Offset.GetValue(context);
            var radius = Radius.GetValue(context);
            var radiusOffset = RadiusOffset.GetValue(context);
            
            //var index = 0;
            var thickness = W.GetValue(context);

            var angelInRads = StartAngel.GetValue(context) * MathUtils.ToRad;
            var deltaAngle = DeltaAngel.GetValue(context) * MathUtils.ToRad / count;
            
            for (var index = 0; index < count; index++)
            {
                var f = count == 1 ? 1 : (float)index / (count-1);
                var length = MathUtils.Lerp(radius, radius + radiusOffset, f);
                var v = Vector3.UnitX * length; 
                var rot = Quaternion.CreateFromAxisAngle(axis,angelInRads);
                var vInAxis = Vector3.Transform(v, rot) + Vector3.Lerp(center, center + offset, f);
                _points[index] = new Vector4(
                                             vInAxis.X,
                                             vInAxis.Y,
                                             vInAxis.Z,
                                             thickness);
                //index++;
                angelInRads += deltaAngle;
            }


            Result.Value = _points;
        }

        private SharpDX.Vector4[] _points = new Vector4[0];

        [Input(Guid = "cb697476-36df-44ae-bd1d-138cc49467c2")]
        public readonly InputSlot<int> Count = new InputSlot<int>();
        
        [Input(Guid = "9C26FCAD-EF7D-46AA-9A7E-EB853E88E955")]
        public readonly InputSlot<float> Radius = new InputSlot<float>();

        [Input(Guid = "BCE00400-5951-4574-AF61-B24FF0AD5E23")]
        public readonly InputSlot<float> RadiusOffset = new InputSlot<float>();
        
        [Input(Guid = "D68DCBA4-D713-4BC7-A418-85042EFC26D3")]
        public readonly InputSlot<Vector3> Center = new InputSlot<Vector3>();
        
        [Input(Guid = "03A54164-8EF9-4CC8-88F3-55AA5DB3640C")]
        public readonly InputSlot<Vector3> Offset = new InputSlot<Vector3>();

        [Input(Guid = "E3736CED-D10D-41F3-92ED-DDFC3EDD1BC6")]
        public readonly InputSlot<float> StartAngel = new InputSlot<float>();

        [Input(Guid = "C9341B17-5F56-4112-BA87-FE734B7BF0BA")]
        public readonly InputSlot<float> DeltaAngel = new InputSlot<float>();
        
        [Input(Guid = "D1E78447-E110-4CDF-B761-DFF32F05140D")]
        public readonly InputSlot<Vector3> Axis = new InputSlot<Vector3>(Vector3.UnitZ);
        
        [Input(Guid = "DEE89AD4-1516-40D0-A682-98E05A8B7C12")]
        public readonly InputSlot<float> W = new InputSlot<float>();
    }
}
