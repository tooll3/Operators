using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Win32;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Vector4 = SharpDX.Vector4;

namespace T3.Operators.Types.Id_796a5efb_2ccf_4cae_b01c_d3f20a070181
{
    public class LinearPoints : Instance<LinearPoints>
    {
        [Output(Guid = "A67DF589-3C51-49A7-805D-5CC0657D491C")]
        public readonly Slot<Point[]> Result = new Slot<Point[]>();

        public LinearPoints()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var countX = CountX.GetValue(context).Clamp(1, 10000);
            var countY = CountY.GetValue(context).Clamp(1, 1);

            var count = countX * countY;
            if (_points.Length != count)
                _points = new T3.Core.DataTypes.Point[count];

            var startP = Start.GetValue(context);
            var endP = Scale.GetValue(context);
            var startW = StartW.GetValue(context);
            var scaleW = ScaleW.GetValue(context);

            var startPoint = new Vector4(startP.X, startP.Y, startP.Z, startW);
            var endPoint = new Vector4(endP.X, endP.Y, endP.Z, scaleW);
                    var index = 0;
                    for (var x = 0; x < countX; x++)
                    {
                        var fX =x / (float)countX;
                        _points[index].Position = SharpDX.Vector4.Lerp( startPoint, endPoint, fX);
                        index++;
                    }

            Result.Value = _points;
        }


        private Point[] _points = new T3.Core.DataTypes.Point[0];



        [Input(Guid = "A32B0085-4F83-4240-845C-D663240A738C")]
        public readonly InputSlot<Vector3> Start = new InputSlot<Vector3>();
        
        [Input(Guid = "4E3863D3-E295-472A-99BB-4C579A4FFD7B")]
        public readonly InputSlot<float> StartW = new InputSlot<float>();

        [Input(Guid = "91D2F5B3-D2C4-406B-AB4C-EBB09951538A")]
        public readonly InputSlot<Vector3> Scale = new InputSlot<Vector3>();
        
        [Input(Guid = "668C5640-A212-40E0-B68E-D78DBCDDE33A")]
        public readonly InputSlot<float> ScaleW = new InputSlot<float>();

        [Input(Guid = "759BFAAC-13DD-478A-A4DB-FE52B94CDAEC")]
        public readonly InputSlot<int> CountX = new InputSlot<int>();
        
        [Input(Guid = "857E953B-A08A-4C32-B201-122CB8D556C6")]
        public readonly InputSlot<int> CountY = new InputSlot<int>();
    }
}