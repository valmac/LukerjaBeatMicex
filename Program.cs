using System;
using System.Diagnostics;

namespace valmac.LukerjaBeatMicex
{
    class Program
    {
        static void Main()
        {
            var sw = Stopwatch.StartNew();
            var calculator = new Calculator(162,10000);
            sw.Stop();
            Console.WriteLine("Calcilator intied,  E:{0}", sw.Elapsed);
            sw = Stopwatch.StartNew();
            calculator.Start();
            Console.WriteLine("Calculation copmplete, E:{0}", sw.Elapsed);
            Console.ReadLine();
        }
    }
}
