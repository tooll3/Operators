using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Win32;
using T3.Core;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Vector4 = SharpDX.Vector4;

namespace T3.Operators.Types.Id_796a5efb_2ccf_4cae_b01c_d3f20a070181
{
    public class Points : Instance<Points>
    {
        [Output(Guid = "A67DF589-3C51-49A7-805D-5CC0657D491C")]
        public readonly Slot<SharpDX.Vector4[]> Result = new Slot<SharpDX.Vector4[]>();

        public Points()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var countX = CountX.GetValue(context).Clamp(1, 10000);
            var countY = CountY.GetValue(context).Clamp(1, 1);

            var count = countX * countY;
            if (_points.Length != count)
                _points = new SharpDX.Vector4[count];

            var startP = Start.GetValue(context);
            var endP = Scale.GetValue(context);
            var startW = StartW.GetValue(context);
            var scaleW = ScaleW.GetValue(context);

            var startPoint = new Vector4(startP.X, startP.Y, startP.Z, startW);
            var endPoint = new Vector4(endP.X, endP.Y, endP.Z, scaleW);
            switch ((Modes)Mode.GetValue(context))
            {
                case Modes.Radial:
                {
                    endPoint+= startPoint;
                    var index = 0;
                    for (var x = 0; x < countX; x++)
                    {
                        for (var y = 0; y < countY; y++)
                        {
                            var f = (float)index / count;
                            var fX =x / (float)countX;
                            var p = SharpDX.Vector4.Lerp(startPoint, endPoint, fX);
                            
                            _points[index] = new SharpDX.Vector4((float)Math.Sin(p.Y * Pi2) + (float)Math.Cos(p.Z * Pi2),
                                                                 (float)Math.Cos(p.X * Pi2) + (float)Math.Sin(p.Z * Pi2) ,
                                                                 (float)Math.Sin(p.X * Pi2) + (float)Math.Cos(p.Y * Pi2), 
                                                                 MathUtils.Lerp(startW, scaleW, f));
                            index++;
                        }
                    }
                    break;
                }
                case Modes.Linear:
                {
                    var index = 0;
                    for (var x = 0; x < countX; x++)
                    {
                        var fX =x / (float)countX;
                        _points[index] = SharpDX.Vector4.Lerp( startPoint, endPoint, fX);
                        index++;
                    }
                    break;
                }
                
                case Modes.PerlinNoise:
                {
                    var index = 0;
                    for (var x = 0; x < countX; x++)
                    {
                        var fX =x / (float)countX;
                        _points[index] =  new Vector4(
                                                      MathUtils.PerlinNoise(MathUtils.Lerp(startP.X, endP.X, fX), 1,2,2),
                                                      MathUtils.PerlinNoise(MathUtils.Lerp(startP.Y, endP.Y, fX), 1,2,2),
                                                      MathUtils.PerlinNoise(MathUtils.Lerp(startP.Z, endP.Z, fX), 1,2,2),
                                                      MathUtils.Lerp(startW, scaleW, fX));
                        index++;
                    }
                    break;
                }
            }

            Result.Value = _points;
        }


        private const float Pi2 = (float)Math.PI * 2;
        private SharpDX.Vector4[] _points = new Vector4[0];

        enum Modes
        {
            Grid,
            Radial,
            Linear,
            PerlinNoise,
            Random,
        }
        
        [Input(Guid = "D50835E0-E417-4777-87BB-1F2BDBC3931A", MappedType = typeof(Modes))]
        public readonly InputSlot<int> Mode = new InputSlot<int>();

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