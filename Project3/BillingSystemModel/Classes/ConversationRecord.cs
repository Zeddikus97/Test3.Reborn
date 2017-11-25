using Project3.BillingSystemModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.BillingSystemModel
{
    class ConversationRecord : IConversationRecord
    {
        public string OutgoingPhoneNumber { get; private set; }
        public string ReceivingPhoneNumber { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}
