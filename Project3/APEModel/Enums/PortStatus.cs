using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.APEModel.Enums
{
    enum PortStatus
    {
        Available = 0,
        IncomingCall,
        Busy,
        DisconnectFromTerminal,
        Unavailable
    }
}
