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
        public AbonentStatus ResponseStatus { get; set; }

        public Port(PortStatus portStatus)
        {
            _portStatus = portStatus;
        }

        public event EventHandler<CallEventArgs> CallConnectEvent;
        public event EventHandler<IncomingCallEventArgs> TakeCallConnectEvent;
        public event EventHandler<EndCallEventArgs> EndCallConnectEvent;

        public PortStatus GetPortStatus()
        {
            return _portStatus;
        }

        public void ConnectToTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.DisconnectedFromTerminal)
            {
                terminal.CallEvent += CallConnectToATE;
                terminal.EndCallEvent += EndCallConnectEvent;
                terminal.ResponseToCallEvent += ResponseToCall;
                _portStatus = PortStatus.Available;
            }

        }

        public void ChangePortStatus(PortStatus status)
        {
            _portStatus = status;
        }

        private void ChangeResponse(AbonentStatus status)
        {
            ResponseStatus = status;
        }

        public void DisconnectFromTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.Available)
            {
                terminal.CallEvent -= CallConnectToATE;
                terminal.EndCallEvent -= EndCallConnectEvent;
                terminal.ResponseToCallEvent -= ResponseToCall;
                _portStatus = PortStatus.DisconnectedFromTerminal;
            }
        }

        protected virtual void RaiseCallConnectEvent(string outgoingPhoneNumber, string receivingPhoneNumber)
        {
            CallConnectEvent?.Invoke(this, new CallEventArgs(outgoingPhoneNumber, receivingPhoneNumber));
        }

        protected virtual void RaiseTakeCallConnectEvent(string outgoingPhoneNumber)
        {
            if (CallConnectEvent != null)
            {
                ChangePortStatus(PortStatus.IncomingCall);
                TakeCallConnectEvent(this, new IncomingCallEventArgs(outgoingPhoneNumber));
            }
        }

        private void CallConnectToATE(object sender, CallEventArgs e)
        {
            RaiseCallConnectEvent(e.OutgoingPhoneNumber, e.ReceivingPhoneNumber);
        }

        private void ResponseToCall(object sender, ResponseStateEventArgs e)
        {
            ChangeResponse(e.Status);
        }

        public void CallConnectToTerminal(string incomingNumber)
        {
            RaiseTakeCallConnectEvent(incomingNumber);
        }
    }
}

