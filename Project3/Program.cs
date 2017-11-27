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
            var introvert = new Rate("Introvert", 0, 6);
            ATE.AddNewRate(lite);
            ATE.AddNewRate(introvert);
            Terminal t1 = ATE.ConcludeContract("Mihail","01", 20, lite);
            Terminal t2 = ATE.ConcludeContract("Igor", "02", 30, lite);
            Terminal t3 = ATE.ConcludeContract("Oksana", "03", 25, lite);
            t1.TurnOn();
            t2.TurnOn();
            t3.TurnOn();
            t1.Call("02");
            t3.Call("01");
            Thread.Sleep(60);
            t2.TurnOff();
            t1.Call("02");
            t1.Call("03");
            t1.EndCall();
            t1.GetCallsHistory();
            t1.GetRateInfo();
            t1.ChangeRate("introvert");
            
            Console.ReadLine();
        }
    }
}
