using Project3.APEModel.Enums;
using Project3.NewEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.APEModel.Classes
{
    class Port
    {
        private PortStatus _portStatus;

        public Port(PortStatus portStatus)
        {
            _portStatus = portStatus;
        }

        public event EventHandler<CallEventArgs> CallConnectEvent;
        public event EventHandler<CallEventArgs> TakeCallConnectEvent;
        public event EventHandler<CallEventArgs> EndCallConnectEvent;

        public PortStatus GetPortStatus()
        {
            return _portStatus;
        }

        public void ConnectToTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.DisconnectFromTerminal)
            {
                terminal.CallEvent += CallConnectToATE;
            }

        }

        public void ChangePortStatus(PortStatus status)
        {
            _portStatus = status;
        }

        public void DisconnectFromTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.Available)
            {
                terminal.CallEvent -= CallConnectToATE;
            }
        }

        protected virtual void RaiseCallConnectEvent(string outgoingPhoneNumber, string receivingPhoneNumber)
        {
            if (CallConnectEvent != null)
            {
                ChangePortStatus(PortStatus.Busy);
                CallConnectEvent(this, new CallEventArgs(outgoingPhoneNumber, receivingPhoneNumber));
            }
        }

        private void CallConnectToATE(object sender, CallEventArgs e)
        {
            RaiseCallConnectEvent(e.OutgoingPhoneNumber, e.ReceivingPhoneNumber);
        }

        public void AnswerCallConnect(string incomingNumber)
        {
            RaiseAnswerCallEvent(incomingNumber);
        }
    }
}

