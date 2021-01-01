using System;
using System.Collections.Generic;
using System.Resources;
using Microsoft.Win32;
using SharpDX;
using T3.Core;
using T3.Core.DataTypes;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;
using Point = T3.Core.DataTypes.Point;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;
using Vector4 = SharpDX.Vector4;

namespace T3.Operators.Types.Id_37794826_a099_4af3_90f4_1e49092a09e1
{
    public class GetListItemAttribute : Instance<GetListItemAttribute>
    {
        [Output(Guid = "0C83599F-1E0F-4BBF-B662-56B4CA5099B0")]
        public readonly Slot<float> Result = new Slot<float>();

        public GetListItemAttribute()
        {
            Result.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var list = DataList.GetValue(context);
            if (list == null || list.NumElements == 0)
                return;

            var index = ItemIndex.GetValue(context) % list.NumElements;

            var requestedFieldIndex = FieldIndex.GetValue(context);
            var requestedFieldName = OrFieldName.GetValue(context);
            
            var fieldIndex = 0;
            foreach (var field in list.Type.GetFields())
            {
                if (fieldIndex == requestedFieldIndex || field.Name == requestedFieldName)
                {
                    var item = list[index];
                    if (field.GetValue(item) is float f)
                    {
                        Result.Value = f;
                        return;
                    }
                }
                fieldIndex++;
            }
        }

        [Input(Guid = "9CF1AE77-1E80-443A-BB07-F545C3D2E71D")]
        public readonly InputSlot<StructuredList> DataList = new InputSlot<StructuredList>();
        
        [Input(Guid = "09F5501D-5F51-4406-B0CD-4C3E572A376F")]
        public readonly InputSlot<int> ItemIndex = new InputSlot<int>();

        [Input(Guid = "6A988C18-DFE1-4F52-9019-ED97BD61A6E8")]
        public readonly InputSlot<int> FieldIndex = new InputSlot<int>();
        
        [Input(Guid = "C6B58AB1-7311-4CF9-B013-C021683FA159")]
        public readonly InputSlot<string> OrFieldName = new InputSlot<string>();

    }
}