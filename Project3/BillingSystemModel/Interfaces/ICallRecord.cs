using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.BillingSystemModel.Interfaces
{
    interface ICallRecord
    {
        string OutgoingPhoneNumber { get; }
        string ReceivingPhoneNumber { get; }
        DateTime Date { get;  }
    }
}
