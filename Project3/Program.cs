using Project3.APEModel.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Program
    {
        static void Main(string[] args)
        {
            AutomaticPhoneExchange ATE = new AutomaticPhoneExchange();
            Terminal t1 = ATE.CreateNewTerminal("01");
            Terminal t2 = ATE.CreateNewTerminal("02");
            t1.TurnOn();
            t2.TurnOn();
            t1.Call("02");
            Console.ReadLine();
        }
    }
}
