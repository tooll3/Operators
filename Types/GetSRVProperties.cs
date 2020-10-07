using System;
using SharpDX;
using SharpDX.Direct3D11;
using T3.Core;
using T3.Core.Logging;
using T3.Core.Operator;
using T3.Core.Operator.Attributes;
using T3.Core.Operator.Slots;

namespace T3.Operators.Types.Id_bc489196_9a30_4580_af6f_dc059f226da1
{
    public class GetSRVProperties : Instance<GetSRVProperties>
    {
        [Output(Guid = "431B39FD-4B62-478B-BBFA-4346102C3F61")]
        public readonly Slot<int> ElementCount = new Slot<int>();

        public GetSRVProperties()
        {
            ElementCount.UpdateAction = Update;
        }

        private void Update(EvaluationContext context)
        {
            var srv = SRV.GetValue(context);
            if (srv == null)
                return;

            ElementCount.Value = srv.Description.Buffer.ElementCount;
        }

        [Input(Guid = "E79473F4-3FD2-467E-ACDA-B27EF7DAE6A9")]
        public readonly InputSlot<ShaderResourceView> SRV = new InputSlot<ShaderResourceView>();
    }
}