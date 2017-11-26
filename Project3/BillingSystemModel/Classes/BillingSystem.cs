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

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(ICallRecord item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ICallRecord item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ICallRecord[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ICallRecord> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(ICallRecord item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
