using Project3.APEModel.Classes;
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
        private string _num = "0";
        private ICollection<Port> _freePorts;
        private IDictionary<Contract, Port> _contracts;
        private ICollection<CallConnection> _connections;


        public void CreateCallConnection(object sender, CallEventArgs e)
        {
            if (_contracts.Keys.Select(x=>x.Number).Contains(e.ReceivingPhoneNumber))
            {
                Port receivingPort = _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()];
                if (receivingPort.GetPortStatus()==PortStatus.Available)
                {
                    _contracts[_contracts.Keys.Where(x => x.Number == e.OutgoingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);
                    _contracts[_contracts.Keys.Where(x => x.Number == e.ReceivingPhoneNumber).First()].ChangePortStatus(PortStatus.Busy);

                }
            }
        }

        public void BreakCallConnection(object sender, CallEventArgs e)
        {

        }

        public void AddNewPort()
        {
            Port newPort = new Port(PortStatus.Unavailable);
            newPort.CallConnectEvent += CreateCallConnection;
            newPort.EndCallConnectEvent += BreakCallConnection;
            _freePorts.Add(newPort);
        }

        public void AddNewTerminal(string number)
        {

        }

        public void ConcludeContract(string name)
        {
            Contract newContract = new Contract(name, _num);
                
        }
    }
}
