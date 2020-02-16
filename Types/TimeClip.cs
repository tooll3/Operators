using System;
using SharpDX;
using T3.Core;
using T3.Core.Animation;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;

namespace T3.Operators.Types.Id_3036067a_a4c2_434b_b0e3_ac95c5c943f4
{
    public class TimeClip : Instance<TimeClip> , ITimeClip
    {
        [Output(Guid = "de6ff8b5-40fe-47fa-b9f2-d926b17f9a7f")]
        public readonly Slot<Command> Output = new Slot<Command>();

        #region implement time clip        
        public TimeRange TimeRange
        {
            get { return new TimeRange(TimeIn.Value, TimeOut.Value); }
            set {  }// TODO: implement
        }
        public TimeRange SourceRange { get; set; }
        #endregion
        
        public TimeClip()
        {
            Output.UpdateAction = Update;
            Output.DirtyFlag.Trigger = DirtyFlagTrigger.Always;
        }

        private void Update(EvaluationContext context)
        {
            var timeIn = TimeIn.GetValue(context);
            var timeOut = TimeOut.GetValue(context);

            if (timeIn > context.TimeInBars || timeOut < context.TimeInBars)
            {
                return;
            } 
            Command.Value?.PrepareAction?.Invoke(context);
            Command.GetValue(context); 
            Command.Value?.RestoreAction?.Invoke(context);
        }

        [Input(Guid = "35f501f4-5c79-4628-9441-8b3782544bf6")]
        public readonly InputSlot<T3.Core.Command> Command = new InputSlot<T3.Core.Command>();

        [Input(Guid = "d6a1b3c0-2a21-425e-a932-f82cd39aeb72")]
        public readonly InputSlot<float> TimeIn = new InputSlot<float>();

        [Input(Guid = "fbc5415b-a733-43ad-b036-fd97d641256a")]
        public readonly InputSlot<float> TimeOut = new InputSlot<float>();

        [Input(Guid = "ed9765be-f8b6-40c1-bfca-22892d0ccd42")]
        public readonly InputSlot<float> SourceTimeIn = new InputSlot<float>();

        [Input(Guid = "227e859a-6a78-4fa0-a134-2f04c5cebc9d")]
        public readonly InputSlot<float> SourceTimeOut = new InputSlot<float>();
    }
}