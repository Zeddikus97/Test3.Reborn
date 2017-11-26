using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.APEModel.Classes
{
    class Rate
    {
        public string Name { get; private set; }
        public decimal CostFirstMinute { get; private set; }
        public decimal CostPerMinute { get; private set; }

        public Rate(string name, decimal costFirstMinute, decimal costPerMinute)
        {
            Name = name;
            CostFirstMinute = costFirstMinute;
            CostPerMinute = costPerMinute;
        }

        public decimal ConversationCost(TimeSpan timeConversation)
        {
            return CostFirstMinute + Convert.ToInt32(timeConversation.TotalMinutes)*CostPerMinute;
        }
    }
}
