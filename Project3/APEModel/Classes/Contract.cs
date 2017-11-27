using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.APEModel.Classes
{
    class Contract
    {
        public string Name { get; private set; }
        public string Number { get; private set; }
        public decimal Balance { get; private set; }
        public decimal Owing { get; private set; }
        public DateTime RateChangeDate { get; private set; }
        public DateTime LastPaymentDate { get; private set; }
        public Rate Rate { get; private set; }

        public Contract(string name, string number, decimal balance, DateTime rateChangeDate, DateTime lastPaymentDate, Rate rate)
        {
            Name = name;
            Number = number;
            Balance = balance;
            Rate = rate;
            RateChangeDate = rateChangeDate;
            LastPaymentDate = lastPaymentDate;
            Owing = 0;
        }

        public void RateChange(Rate rate)
        {
            Rate = rate;
        }

        public void ChangeBalance(decimal balance)
        {
            Balance += balance;
        }

        public void Exact(DateTime timeNow)
        {
            if ((timeNow.Month - LastPaymentDate.Month + 12 * (timeNow.Year - LastPaymentDate.Year)) > 0)
            {
                Balance -= Owing;
                Owing = 0;
                LastPaymentDate = timeNow;
            }      
        }

        public decimal ChangeBalance(TimeSpan timeConversation)
        {
            var cost = Rate.ConversationCost(timeConversation);
            Owing -= cost;
            return cost;           
        }
    }
}
