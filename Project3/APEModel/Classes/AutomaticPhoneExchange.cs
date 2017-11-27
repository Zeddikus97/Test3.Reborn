using Project3.APEModel.Classes;
using Project3.APEModel.Enums;
using Project3.BillingSystemModel;
using Project3.NewEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.APEModel.Classes
{
    class AutomaticPhoneExchange
    {
        private ICollection<Port> _freePorts = new List<Port>();
        private IDictionary<Contract, Port> _contracts = new Dictionary<Contract, Port>();
        private ICollection<CallRecord> _connections = new List<CallRecord>();
        private ICollection<Rate> _rateTypes = new List<Rate>();
        public BillingSystem ThisBillingSystem { get; private set; }

        public AutomaticPhoneExchange(BillingSystem billingsystem)
        {
            ThisBillingSystem = billingsystem;
        }

        private void CreateCallConnection(object sender, CallEventArgs e)
        {
            if (e.ReceivingPhoneNumber==e.OutgoingPhoneNumber|| (sender as Port).GetPortStatus()==PortStatus.IncomingCall)
            {
                if (sender is Port) (sender as Port).MessageToTerminal("Connection failed");
            }
            else if (_contracts.Keys.Select(x=>x.Number).Contains(e.ReceivingPhoneNumber))
            {
                var connection = new CallRecord(e.OutgoingPhoneNumber, e.ReceivingPhoneNumber, DateTime.Now);
                Port receivingPort = _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()];
                _contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First().Exact(DateTime.Now);
                if (!(_contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First().Balance>0))
                {
                    if (sender is Port) (sender as Port).MessageToTerminal("You doesn't have enough money. Please, reise the balance.");
                }                  
                else if (receivingPort.GetPortStatus()==PortStatus.Available)
                {
                    receivingPort.CallConnectToTerminal(e.OutgoingPhoneNumber);
                    if (receivingPort.ResponseStatus == AbonentStatus.ReadyToConnect)
                    {
                        _contracts[_contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);
                        _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);
                        _connections.Add(connection);
                    }
                    else
                    {
                        if (sender is Port) (sender as Port).MessageToTerminal("Subscriber ignored this call");
                        AddCallRecord(connection);
                    }
                }                
                else if (receivingPort.GetPortStatus() == PortStatus.Busy || receivingPort.GetPortStatus() == PortStatus.IncomingCall)
                {
                    if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number is busy now");
                    AddCallRecord(connection);
                }
                else if (receivingPort.GetPortStatus() == PortStatus.DisconnectedFromTerminal)
                {
                    if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number is disconnected now");
                    AddCallRecord(connection);
                }
            }
            else
            {
                if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number doesn't exist");          
            }
        }

        private void BreakCallConnection(object sender, RequestEventArgs e)
        {
            if (_connections.Select(x=>x.OutgoingPhoneNumber).Contains(e.OutgoingPhoneNumber)||_connections.Select(x => x.ReceivingPhoneNumber).Contains(e.OutgoingPhoneNumber))
            {
                var connection = _connections.Where(x => x.OutgoingPhoneNumber == e.OutgoingPhoneNumber || x.ReceivingPhoneNumber == e.OutgoingPhoneNumber).First();
                var cost = _contracts.Keys.Where(x => x.Number == connection.OutgoingPhoneNumber).First().ChangeBalance(DateTime.Now - connection.Date);
                ThisBillingSystem.Add(new ConversationRecord(connection.OutgoingPhoneNumber, connection.ReceivingPhoneNumber, connection.Date, DateTime.Now - connection.Date, cost));
                
                _contracts[_contracts.Keys.Where(x => x.Number == connection.ReceivingPhoneNumber).First()].ChangePortStatus(PortStatus.Available);
                if (_contracts[_contracts.Keys.Where(x => x.Number == connection.OutgoingPhoneNumber).First()].GetPortStatus() != PortStatus.DisconnectedFromTerminal)
                {
                    _contracts[_contracts.Keys.Where(x => x.Number == connection.OutgoingPhoneNumber).First()].ChangePortStatus(PortStatus.Available);
                }
                _connections.Remove(connection);              
            }
        }

        private void GetCallInformation(object sender, GetCallInfoEventArgs e)
        {
            if (e.Type==CallInformationType.Incoming)
                if (sender is Port) (sender as Port).MessageToTerminal(ThisBillingSystem.GetIncomingCallsHistory(e.OutgoingPhoneNumber));
            if (e.Type == CallInformationType.Outgoing)
                if (sender is Port) (sender as Port).MessageToTerminal(ThisBillingSystem.GetOutgoingCallsHistory(e.OutgoingPhoneNumber));
            if (e.Type == CallInformationType.All)
                if (sender is Port) (sender as Port).MessageToTerminal(ThisBillingSystem.GetAllCallsHistory(e.OutgoingPhoneNumber));
            if (e.Type == CallInformationType.Filtered)
                if (sender is Port) (sender as Port).CallEnumerableToTerminal(ThisBillingSystem.Get());
        }

        private void GetRateInformation(object sender, EventArgs e)
        {
            if (sender is Port) (sender as Port).MessageToTerminal(String.Join("\n", _rateTypes.Select(x => x.Name + ": first minute - " + x.CostFirstMinute + ", other minutes - " + x.CostPerMinute)));
        }

        private void ChangeContractRate(object sender, ChangeRateEventArgs e)
        {
            Rate rateForChange=null;
            int index=0;
            foreach (var rate in _rateTypes)
            {
                if (rate.Name == e.Rate || rate.Name.ToLower() == e.Rate || index.ToString() == e.Rate) rateForChange = rate;
                index++;
            }
            if (rateForChange != null)
            {
                var contract = _contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First();
                
                if (((DateTime.Now.Month - contract.RateChangeDate.Month) + 12* (DateTime.Now.Month - contract.RateChangeDate.Year))>1)
                {
                    _contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First().RateChange(rateForChange);
                }
                else if (sender is Port) (sender as Port).MessageToTerminal("Less than a month since the last tariff change");
            }
            else if (sender is Port) (sender as Port).MessageToTerminal("This rate doesn't exist");
        }

        private void ReiseBalance(object sender, ReiseBalanceEventArgs e)
        {
            _contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First().ChangeBalance(e.Money);
        }

        private void AddCallRecord(CallRecord item)
        {
           ThisBillingSystem.Add(item);
        }       

        public void AddNewRate(string name, decimal costFirstMinute, decimal costPerMinute)
        {
            if (!_rateTypes.Select(x => x.Name).Contains(name)) _rateTypes.Add(new Rate(name, costFirstMinute, costPerMinute));
        }

        public void AddNewRate(Rate rate)
        {
            if (!_rateTypes.Select(x => x.Name).Contains(rate.Name)) _rateTypes.Add(rate);
        }

        public void AddNewPort()
        {
            Port newPort = new Port(PortStatus.DisconnectedFromTerminal);
            newPort.CallConnectEvent += CreateCallConnection;
            newPort.EndCallConnectEvent += BreakCallConnection;
            newPort.GetRateInfoConnectEvent += GetRateInformation;
            newPort.ReiseBalanceConnectEvent += ReiseBalance;
            newPort.ChangeRateConnectEvent += ChangeContractRate;
            newPort.GetCallInfoConnectEvent += GetCallInformation;
            _freePorts.Add(newPort);
        }

        public Terminal CreateNewTerminal(string number, Contract contract)
        {
            AddNewPort();
            var term = new Terminal(number, _freePorts.First());
            _contracts.Add(contract, _freePorts.First());
            _freePorts.Remove(_freePorts.First());
            return term;
        }

        public Terminal ConcludeContract(string name, string number, decimal balance, Rate rate)
        {
            Contract newContract = new Contract(name, number, balance, DateTime.Now, DateTime.Now, rate);
            return CreateNewTerminal(number, newContract);    
        }
    }
}
