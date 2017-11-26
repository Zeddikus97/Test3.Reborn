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

        public string GetIncomingCallsHistory()
        {
            return String.Join("\n", _records.Select(x => "from " + x.ReceivingPhoneNumber + " // " + x.ToString(true)));
        }

        public string GetOutgoingCallsHistory()
        {
            return String.Join("\n", _records.Select(x => "from " + x.OutgoingPhoneNumber + " // " + x.ToString(false)));
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
