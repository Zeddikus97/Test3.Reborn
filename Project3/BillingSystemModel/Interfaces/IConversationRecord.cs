using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.BillingSystemModel.Interfaces
{
    interface IConversationRecord : ICallRecord
    {
        TimeSpan Duration { get; }
        decimal Cost { get; }
    }
}
