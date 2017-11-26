﻿using Project3.APEModel.Classes;
using Project3.APEModel.Enums;
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
        private ICollection<CallConnection> _connections = new List<CallConnection>();


        private void CreateCallConnection(object sender, CallEventArgs e)
        {
            Console.WriteLine("ixhto");
            if (_contracts.Keys.Select(x=>x.Number).Contains(e.ReceivingPhoneNumber))
            {               
                Port receivingPort = _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()];
                if (receivingPort.GetPortStatus()==PortStatus.Available)
                {
                    receivingPort.CallConnectToTerminal(e.OutgoingPhoneNumber);
                    if (receivingPort.ResponseStatus == AbonentStatus.ReadyToConnect)
                    {
                        _contracts[_contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);
                        _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);
                        _connections.Add(new CallConnection(e.OutgoingPhoneNumber, e.ReceivingPhoneNumber, DateTime.Now));
                        Console.WriteLine(String.Join(" ", e.OutgoingPhoneNumber + e.ReceivingPhoneNumber + DateTime.Now));
                    }
                }                
                else if (receivingPort.GetPortStatus() == PortStatus.Busy || receivingPort.GetPortStatus() == PortStatus.IncomingCall)
                {
                    if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number is busy now");                    
                }
                else if (receivingPort.GetPortStatus() == PortStatus.DisconnectedFromTerminal)
                {
                    if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number is disconnected now");
                }
            }
            else
            {
                if (sender is Port) (sender as Port).MessageToTerminal("Subscriber with this number doesn't exist");
            }
        }

        public void BreakCallConnection(object sender, EndCallEventArgs e)
        {
            if (_connections.Select(x=>x.OutgoingPhoneNumber).Contains(e.OutgoingPhoneNumber)||_connections.Select(x => x.ReceivingPhoneNumber).Contains(e.OutgoingPhoneNumber))
            {
                _connections.Where(x => x.OutgoingPhoneNumber == e.OutgoingPhoneNumber||x.ReceivingPhoneNumber == e.OutgoingPhoneNumber)

                /*_contracts[_contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First()].ChangePortStatus(PortStatus.Available);
                _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()].ChangePortStatus(PortStatus.Available);*/
            }

        }

        public void AddNewPort()
        {
            Port newPort = new Port(PortStatus.DisconnectedFromTerminal);
            newPort.CallConnectEvent += CreateCallConnection;
            newPort.EndCallConnectEvent += BreakCallConnection;
            _freePorts.Add(newPort);
        }

        public Terminal CreateNewTerminal(string number)
        {
            AddNewPort();
            var term = new Terminal(number, _freePorts.First());
            _contracts.Add(ConcludeContract(number), _freePorts.First());
            _freePorts.Remove(_freePorts.First());
            return term;
        }

        public Contract ConcludeContract(string number)
        {
            Contract newContract = new Contract("vasya", number);
            return newContract;           
        }
    }
}
