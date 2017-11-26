using Project3.APEModel.Enums;
using Project3.NewEventArgs; using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;  namespace Project3.APEModel.Classes {     class Terminal     {         string _number;
        Port ThisPort { get; set; }

        public Terminal(string number, Port port)
        {
            _number = number;
            ThisPort = port;
        }

        public event EventHandler<CallEventArgs> CallEvent;         public event EventHandler<ResponseStateEventArgs> ResponseToCallEvent;         public event EventHandler<EndCallEventArgs> EndCallEvent;                protected virtual void RaiseCallEvent(string outgoingPhoneNumber)         {             CallEvent?.Invoke(this, new CallEventArgs(_number, outgoingPhoneNumber));         }          protected virtual void RaiseResponseToCallEvent(AbonentStatus status)         {             ResponseToCallEvent?.Invoke(this, new ResponseStateEventArgs(status));         }          protected virtual void RaiseEndCallEvent()         {             EndCallEvent?.Invoke(this, new EndCallEventArgs(_number));         }          private void ConnectToPort()         {             ThisPort.ConnectToTerminal(this);             ThisPort.TakeCallConnectEvent += TakeIncomingCall;         }          private void DisconnectFromPort()         {             ThisPort.DisconnectFromTerminal(this);             ThisPort.TakeCallConnectEvent -= TakeIncomingCall;         }          public void Call(string number)         {                         RaiseCallEvent(number);         }          public void EndCall()         {             RaiseEndCallEvent();         }          public void TakeIncomingCall(object sender, IncomingCallEventArgs e)         {             Console.WriteLine("Вам звонит номер " + e.OutgoingPhoneNumber);             Console.WriteLine("Принять звонок? ");             string taxe = Console.ReadLine();             if(taxe=="1")             {
                RaiseResponseToCallEvent(AbonentStatus.ReadyToConnect);
            }             else
            {
                RaiseResponseToCallEvent(AbonentStatus.Abandoned);
            }         }          public void TurnOn()
        {
            ConnectToPort();
        }          public void TurnOff()
        {
            DisconnectFromPort();
        }     } } 