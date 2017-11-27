using Project3.APEModel.Enums;
using Project3.NewEventArgs; using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;  namespace Project3.APEModel.Classes {     class Terminal     {
        private Port _port;         public string Number { get; private set; }  

        public Terminal(string number, Port port)
        {
            Number = number;
            _port = port;
        }

        public event EventHandler<CallEventArgs> CallEvent;                 public event EventHandler<ResponseToCallEventArgs> ResponseToCallEvent;         public event EventHandler<RequestEventArgs> EndCallEvent;
        public event EventHandler<ChangeRateEventArgs> ChangeRateEvent;
        public event EventHandler<ReiseBalanceEventArgs> ReiseBalanceEvent;
        public event EventHandler GetRateInfoEvent;
        public event EventHandler<GetCallInfoEventArgs> GetCallInfoEvent;

        protected virtual void OnReiseBalanceEvent(decimal money)         {             ReiseBalanceEvent?.Invoke(this, new ReiseBalanceEventArgs(money, Number));         }

        protected virtual void OnChangeRateEvent(string nameOfRate)         {             ChangeRateEvent?.Invoke(this, new ChangeRateEventArgs(nameOfRate, Number));         }          protected virtual void OnCallEvent(string receivingPhoneNumber)
        {
            CallEvent?.Invoke(this, new CallEventArgs(Number, receivingPhoneNumber));
        }          protected virtual void OnResponseToCallEvent(AbonentStatus status)         {             ResponseToCallEvent?.Invoke(this, new ResponseToCallEventArgs(status));         }          protected virtual void OnEndCallEvent()         {             EndCallEvent?.Invoke(this, new RequestEventArgs(Number));         }          protected virtual void OnGetRateInfoEvent()         {             GetRateInfoEvent?.Invoke(this, EventArgs.Empty);         }          protected virtual void OnGetCallInfoEvent(CallInformationType type)         {             GetCallInfoEvent?.Invoke(this, new GetCallInfoEventArgs(Number, type));         }          private void ConnectToPort()         {             _port.ConnectToTerminal(this);             _port.TakeCallConnectEvent += TakeIncomingCall;             _port.TakeIncomingMessageEvent += ShowIncomingMessage;         }          private void DisconnectFromPort()         {             _port.DisconnectFromTerminal(this);             _port.TakeCallConnectEvent -= TakeIncomingCall;             _port.TakeIncomingMessageEvent -= ShowIncomingMessage;         }          public void Call(string number)         {                      OnCallEvent(number);         }          public void EndCall()         {             OnEndCallEvent();         }

        public void GetRateInfo()         {             OnGetRateInfoEvent();         }          public void ReiseBalance(decimal money)         {             OnReiseBalanceEvent(money);         }          public void ChangeRate(string nameOfRate)         {             OnChangeRateEvent(nameOfRate);         }          public void TakeIncomingCall(object sender, RequestEventArgs e)         {             Console.WriteLine("Вам звонит номер " + e.OutgoingPhoneNumber);             Console.WriteLine("Принять звонок? ");             string taxe = Console.ReadLine();             if(taxe=="1")             {
                OnResponseToCallEvent(AbonentStatus.ReadyToConnect);
            }             else
            {
                OnResponseToCallEvent(AbonentStatus.Abandoned);
            }         }          public void ShowIncomingMessage(object sender, MessageEventArgs e)         {             Console.WriteLine(e.Message);         }

        public void GetIncomingCallsHistory()
        {
            OnGetCallInfoEvent(CallInformationType.Incoming);
        }          public void GetOutgoingCallsHistory()
        {
            OnGetCallInfoEvent(CallInformationType.Outgoing);
        }          public void GetCallsHistory()
        {
            OnGetCallInfoEvent(CallInformationType.All);
        }          public void TurnOn()
        {            
            ConnectToPort();
        }          public void TurnOff()
        {
            if (_port.GetPortStatus() == PortStatus.Busy) EndCall();
            DisconnectFromPort();
        }     } } 