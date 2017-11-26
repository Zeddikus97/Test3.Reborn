using Project3.APEModel.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class ChangeRateEventArgs : EventArgs, IRequestEventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }
        public string Rate { get; private set; }

        public ChangeRateEventArgs(string rate, string outgoingPhoneNumber)
        {
            OutgoingPhoneNumber = outgoingPhoneNumber;
            Rate = rate;
        }
    }
}
