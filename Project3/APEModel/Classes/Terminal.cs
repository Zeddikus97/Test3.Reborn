using Project3.APEModel.Enums;
using Project3.NewEventArgs; using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;  namespace Project3.APEModel.Classes {     class Terminal     {         string _number;
        public Port ThisPort { get; private set; }

        public Terminal(string number, Port port)
        {
            _number = number;
            ThisPort = port;
        }

        public event EventHandler<CallEventArgs> CallEvent;         public event EventHandler<ResponseToCallEventArgs> ResponseToCallEvent;         public event EventHandler<EndCallEventArgs> EndCallEvent;                protected virtual void RaiseCallEvent(string outgoingPhoneNumber)         {             CallEvent?.Invoke(this, new CallEventArgs(_number, outgoingPhoneNumber));         }          protected virtual void RaiseResponseToCallEvent(AbonentStatus status)         {             ResponseToCallEvent?.Invoke(this, new ResponseToCallEventArgs(status));         }          protected virtual void RaiseEndCallEvent()         {             EndCallEvent?.Invoke(this, new EndCallEventArgs(_number));         }          private void ConnectToPort()         {             ThisPort.ConnectToTerminal(this);             ThisPort.TakeCallConnectEvent += TakeIncomingCall;             ThisPort.TakeIncomingMessageEvent += ShowIncomingMessage;         }          private void DisconnectFromPort()         {             ThisPort.DisconnectFromTerminal(this);             ThisPort.TakeCallConnectEvent -= TakeIncomingCall;             ThisPort.TakeIncomingMessageEvent -= ShowIncomingMessage;         }          public void Call(string number)         {                         RaiseCallEvent(number);         }          public void EndCall()         {             RaiseEndCallEvent();         }          public void TakeIncomingCall(object sender, IncomingCallEventArgs e)         {             Console.WriteLine("Вам звонит номер " + e.OutgoingPhoneNumber);             Console.WriteLine("Принять звонок? ");             string taxe = Console.ReadLine();             if(taxe=="1")             {
                RaiseResponseToCallEvent(AbonentStatus.ReadyToConnect);
            }             else
            {
                RaiseResponseToCallEvent(AbonentStatus.Abandoned);
            }         }          public void ShowIncomingMessage(object sender, MessageEventArgs e)         {             Console.WriteLine(e.Message);         }          public void TurnOn()
        {
            ConnectToPort();
        }          public void TurnOff()
        {
            DisconnectFromPort();
        }     } } 