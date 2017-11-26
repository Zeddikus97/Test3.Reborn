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
        public AbonentStatus ResponseStatus { get; private set; }

        public Port(PortStatus portStatus)
        {
            _portStatus = portStatus;
        }

        public event EventHandler<CallEventArgs> CallConnectEvent;              
        public event EventHandler<RequestEventArgs> EndCallConnectEvent;
        public event EventHandler<ChangeRateEventArgs> ChangeRateConnectEvent;
        public event EventHandler<ReiseBalanceEventArgs> ReiseBalanceConnectEvent;
        public event EventHandler GetRateInfoConnectEvent;

        public event EventHandler<RequestEventArgs> TakeCallConnectEvent;
        public event EventHandler<MessageEventArgs> TakeIncomingMessageEvent;

        public PortStatus GetPortStatus()
        {
            return _portStatus;
        }

        public void ChangePortStatus(PortStatus status)
        {
            _portStatus = status;
        }

        private void ChangeResponse(AbonentStatus status)
        {
            ResponseStatus = status;
        }

        public void ConnectToTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.DisconnectedFromTerminal)
            {
                terminal.CallEvent += CallConnectToATE;
                terminal.EndCallEvent += EndCallConnectEvent;
                terminal.ResponseToCallEvent += ResponseToCall;
                terminal.GetRateInfoEvent += GetRateInfoFromATE;
                terminal.ChangeRateEvent += ChangeRateFromTerminal;
                terminal.ReiseBalanceEvent += ReiseBalanceFromTerminal;
                _portStatus = PortStatus.Available;
                ResponseStatus = AbonentStatus.Abandoned;
           }
        }

        public void DisconnectFromTerminal(Terminal terminal)
        {
            if (_portStatus == PortStatus.Available)
            {
                terminal.CallEvent -= CallConnectToATE;
                terminal.EndCallEvent -= EndCallConnectEvent;
                terminal.ResponseToCallEvent -= ResponseToCall;
                terminal.GetRateInfoEvent -= GetRateInfoFromATE;
                terminal.ChangeRateEvent -= ChangeRateFromTerminal;
                terminal.ReiseBalanceEvent -= ReiseBalanceFromTerminal;
                _portStatus = PortStatus.DisconnectedFromTerminal;
            }
        }

        protected virtual void OnGetRateInfoConnectEvent()
        {
            GetRateInfoConnectEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCallConnectEvent(string outgoingPhoneNumber, string receivingPhoneNumber)
        {
            CallConnectEvent?.Invoke(this, new CallEventArgs(outgoingPhoneNumber, receivingPhoneNumber));
        }

        protected virtual void OnTakeCallConnectEvent(string outgoingPhoneNumber)
        {
            if (TakeCallConnectEvent != null)
            {
                ChangePortStatus(PortStatus.IncomingCall);
                TakeCallConnectEvent(this, new RequestEventArgs(outgoingPhoneNumber));
            }
        }

        protected virtual void OnTakeImcomingMessage(string message)
        {
            TakeIncomingMessageEvent?.Invoke(this, new MessageEventArgs(message));
        }

        protected virtual void OnReiseBalanceConnectEvent(decimal money, string outgoingPhoneNumber)         {             ReiseBalanceConnectEvent?.Invoke(this, new ReiseBalanceEventArgs(money, outgoingPhoneNumber));         }

        protected virtual void OnChangeRateConnectEvent(string rate, string outgoingPhoneNumber)         {
            ChangeRateConnectEvent?.Invoke(this, new ChangeRateEventArgs(rate, outgoingPhoneNumber));         }

        private void ResponseToCall(object sender, ResponseToCallEventArgs e)
        {
            ChangeResponse(e.Status);
        }

        private void GetRateInfoFromATE(object sender, EventArgs e)
        {
            OnGetRateInfoConnectEvent();
        }

        private void CallConnectToATE(object sender, CallEventArgs e)
        {
            OnCallConnectEvent(e.OutgoingPhoneNumber, e.ReceivingPhoneNumber);
        }

        public void ReiseBalanceFromTerminal(object sender, ReiseBalanceEventArgs e)
        {
            OnReiseBalanceConnectEvent(e.Money, e.OutgoingPhoneNumber);
        }

        public void ChangeRateFromTerminal(object sender, ChangeRateEventArgs e)
        {
            OnChangeRateConnectEvent(e.Rate, e.OutgoingPhoneNumber);
        }

        public void CallConnectToTerminal(string incomingNumber)
        {
            OnTakeCallConnectEvent(incomingNumber);
        }

        public void MessageToTerminal(string incomingNumber)
        {
            OnTakeImcomingMessage(incomingNumber);
        }

        

    }
}

