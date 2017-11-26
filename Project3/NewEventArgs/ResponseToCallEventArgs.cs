using Project3.APEModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class ResponseToCallEventArgs : EventArgs
    {
        public AbonentStatus Status { get; private set; }

        public ResponseToCallEventArgs(AbonentStatus status)
        {
            Status = status;
        }
    }
}
