﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.NewEventArgs
{
    class TakeMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public TakeMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
