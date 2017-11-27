using Project3.APEModel.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Project3
{
    class Program
    {
        static void Main(string[] args)
        {
            BillingSystem BS = new BillingSystem();
            AutomaticPhoneExchange ATE = new AutomaticPhoneExchange(BS);
            var lite = new Rate("Lite", 4, 2);
            var short2 = new Rate("Short", 0, 6);
            ATE.AddNewRate(lite);
            ATE.AddNewRate(short2);
            Terminal t1 = ATE.ConcludeContract("Mihail","01", 20, lite);
            Terminal t2 = ATE.ConcludeContract("Igor", "02", 30, lite);
            Terminal t3 = ATE.ConcludeContract("Oksana", "03", 25, short2);
            t1.TurnOn();
            t2.TurnOn();
            t3.TurnOn();
            t1.Call("02");
            Thread.Sleep(2000);
            t3.Call("01");
            Thread.Sleep(2000);
            t2.TurnOff();
            Thread.Sleep(2000);
            t1.Call("02");
            Thread.Sleep(2000);
            t1.Call("03");
            Thread.Sleep(2000);
            t1.EndCall();
            Thread.Sleep(2000);
            t1.GetCallsHistory();
            Thread.Sleep(2000);
            t1.GetRateInfo();
            Thread.Sleep(2000);
            t1.ChangeRate("short");
            Thread.Sleep(2000);
            t1.GetCallsHistoryByCost(4);

            Console.ReadLine();
        }
    }
}
