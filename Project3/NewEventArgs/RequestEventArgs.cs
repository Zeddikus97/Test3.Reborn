using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class RequestEventArgs : EventArgs, IRequestEventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }

        public RequestEventArgs(string outgoingphonenumber)
        {
            OutgoingPhoneNumber = outgoingphonenumber;
        }
    }
}
