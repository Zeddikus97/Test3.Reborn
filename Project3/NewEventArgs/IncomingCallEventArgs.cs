using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class IncomingCallEventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }

        public IncomingCallEventArgs(string outgoingphonenumber)
        {
            OutgoingPhoneNumber = outgoingphonenumber;
        }
    }
}
