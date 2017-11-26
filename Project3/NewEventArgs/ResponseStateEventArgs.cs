using Project3.APEModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class ResponseStateEventArgs
    {
        public AbonentStatus Status { get; private set; }

        public ResponseStateEventArgs(AbonentStatus status)
        {
            Status = status;
        }
    }
}
