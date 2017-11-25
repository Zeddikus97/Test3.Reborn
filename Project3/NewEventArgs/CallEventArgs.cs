using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class CallEventArgs : EventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }
        public string ReceivingPhoneNumber { get; private set; }

        public CallEventArgs(string outgoingphonenumber, string receivingphonenumber)
        {
            OutgoingPhoneNumber = outgoingphonenumber;
            ReceivingPhoneNumber = receivingphonenumber;
        }
    }
}
