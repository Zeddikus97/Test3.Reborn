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
        public DateTime RateChangeDate { get; private set; }
        public Rate Rate { get; private set; }

        public Contract(string name, string number, decimal balance, DateTime rateChangeDate, Rate rate)
        {
            Name = name;
            Number = number;
            Balance = balance;
            Rate = rate;
            RateChangeDate = rateChangeDate;
        }

        public void RateChange(Rate rate)
        {
            Rate = rate;
        }

        public void ChangeBalance(decimal balance)
        {
            Balance = Balance + balance;
        }

        public decimal ChangeBalance(TimeSpan timeConversation)
        {
            var cost = Rate.ConversationCost(timeConversation);
            ChangeBalance(-cost);
            Console.WriteLine(Balance);
            return cost;
            
        }
    }
}
