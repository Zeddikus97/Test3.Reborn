using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class EndCallEventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }

        public EndCallEventArgs(string outgoingphonenumber)
        {
            OutgoingPhoneNumber = outgoingphonenumber;
        }
    }
}
