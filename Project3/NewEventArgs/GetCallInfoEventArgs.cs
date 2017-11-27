using Project3.APEModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class GetCallInfoEventArgs : EventArgs, IRequestEventArgs
    {
        public string OutgoingPhoneNumber { get; }
        public CallInformationType Type { get;  }

        public GetCallInfoEventArgs(string outgoingphonenumber, CallInformationType type)
        {
            OutgoingPhoneNumber = outgoingphonenumber;
            Type = type;
        }
    }
}
