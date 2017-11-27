using Project3.BillingSystemModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class TakeCallEnumerableEventArgs : EventArgs
    {
        public IEnumerable<ICallRecord> CallRecordEnumerable { get; private set; }

        public TakeCallEnumerableEventArgs(IEnumerable<ICallRecord> callRecordEnumerable)
        {
            CallRecordEnumerable = callRecordEnumerable;
        }
    }
}
