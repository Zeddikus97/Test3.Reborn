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
        public decimal Cost { get; private set; }

        public ConversationRecord(string outgoingPhoneNumber, string receivingPhoneNumber, DateTime date, TimeSpan duration, decimal cost)
        {
            OutgoingPhoneNumber = outgoingPhoneNumber;
            ReceivingPhoneNumber = receivingPhoneNumber;
            Date = date;
            Duration = duration;
            Cost = cost;
        }

        public string ToString(bool outcoming)
        {
            if (outcoming == true) return "date:" + Date.ToString() + " duration: " + Duration.ToString() + " cost:" + Cost.ToString();
            else return "date:" + Date.ToString() + " duration: " + Duration.ToString();
        }

    }
}
