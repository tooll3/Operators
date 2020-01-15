using System.Collections.Generic;
using T3.Core.Operator;

namespace T3.Operators.Types.Id_ef357e66_24e9_4f54_8d86_869db74602f4
{
    public class GetFromStringList : Instance<GetFromStringList>
    {
        [Output(Guid = "467bb46e-3391-48a7-b0eb-f7fd9d77b60f")]
        public readonly Slot<string> Selected = new Slot<string>();

        public GetFromStringList()
        {
            Selected.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var list = Input.GetValue(context);
            if (list == null)
            { 
                Selected.Value= string.Empty;
                return;
            }

            var count = list.Count;
            if (count == 0)
            {
                Selected.Value = string.Empty;
                return;
            }
            
            if (count < 0)
                count = -count;

            var index = Index.GetValue(context) % count;
            if (index >= 0 && index < list.Count)
            {
                Selected.Value = list[index];
            }
        }

        [Input(Guid = "8d5e77a6-1ec4-4979-ad26-f7862049bce1")]
        public readonly InputSlot<List<string>> Input = new InputSlot<List<string>>(new List<string>(20));

        [Input(Guid = "12ce5fe3-750f-47ed-9507-416cb327a615")]
        public readonly InputSlot<int> Index = new InputSlot<int>(0);
    }
}