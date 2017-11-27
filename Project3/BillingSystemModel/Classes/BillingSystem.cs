using Project3.BillingSystemModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Project3
{
    class BillingSystem : ICollection<ICallRecord>
    {
        private ICollection<ICallRecord> _records = new List<ICallRecord>();

        public string GetIncomingCallsHistory(string number)
        {
            return String.Join("\n", _records.Where(x=>x.ReceivingPhoneNumber == number).Select(x => "from " + x.OutgoingPhoneNumber + " // " + x.ToString(false)));
        }

        public string GetOutgoingCallsHistory(string number)
        {
            return String.Join("\n", _records.Where(x => x.OutgoingPhoneNumber == number).Select(x => "to " + x.ReceivingPhoneNumber + " // " + x.ToString(true)));
        }

        public string GetAllCallsHistory(string number)
        {
            return String.Join("\n", _records.Where(x => x.OutgoingPhoneNumber == number || x.ReceivingPhoneNumber == number).Select(x => "from " + x.OutgoingPhoneNumber + " to " + x.ReceivingPhoneNumber + " // " + x.ToString(true)));
        }

        public void Clear()
        {
            _records.Clear();
        }

        public int Count
        {
            get
            {
                return _records.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _records.IsReadOnly;
            }
        }

        public void Add(ICallRecord item)
        {
            _records.Add(item);
        }


        public bool Contains(ICallRecord item)
        {
            return _records.Contains(item);
        }

        public void CopyTo(ICallRecord[] array, int arrayIndex)
        {
            _records.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ICallRecord> GetEnumerator()
        {
            return _records.GetEnumerator();
        }

        public bool Remove(ICallRecord item)
        {
            return _records.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
