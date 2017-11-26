using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class ReiseBalanceEventArgs : EventArgs, IRequestEventArgs
    {
        public string OutgoingPhoneNumber { get; private set; }
        public decimal Money { get; private set; }

        public ReiseBalanceEventArgs(decimal money, string outgoingPhoneNumber)
        {
            OutgoingPhoneNumber = outgoingPhoneNumber; 
            Money = money;
        }
    }
}
