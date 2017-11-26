using Project3.BillingSystemModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.BillingSystemModel
{
    class CallRecord : ICallRecord
    {
        public string OutgoingPhoneNumber { get; private set; }
        public string ReceivingPhoneNumber { get; private set; }
        public DateTime Date { get; private set; }

        public CallRecord(string outgoingPhoneNumber, string receivingPhoneNumber, DateTime date)
        {
            OutgoingPhoneNumber = outgoingPhoneNumber;
            ReceivingPhoneNumber = receivingPhoneNumber;
            Date = date;
        }

        public string ToString(bool outgoing)
        {
            if (outgoing == true) return "date:" + Date.ToString() + " - missed";
            else return "date:" + Date.ToString() + " - unaccepted";
        }
    }
}
