using System;
using System.Diagnostics;

namespace valmac.LukerjaBeatMicex
{
    class Program
    {
        private const double  TARGET_YIELD = 162;
        private const int DEFAULT_PORTFOLIO_SIZE = 8;
        private static readonly string TILDAS = new string('~', 70);

        static void Main(string[] args)
        {
            DoIt(args);

            //mcx_indx();
            //for (uint i = 1; i <= 30; ++i) Console.WriteLine("{0}\t{1:N0}",  i, Calculator.CombinationsCount(i,30));
        }

        private static void mcx_indx()
        {
            var mcx = new MicexIndex();
            int anove = 0;
            double sum = 0;
            double k = 1.0/mcx.Items.Count;
            foreach (var item in mcx.Items)
            {
                Console.WriteLine(item);

                sum += item.Chg * k;

                if (item.Chg > 265)
                    ++anove;
            }

            Console.WriteLine(anove);
            Console.WriteLine(sum);
        }

        private static void DoIt(string[] args)
        {
            if (args.Length == 0)
            {
                Process(DEFAULT_PORTFOLIO_SIZE);
            }
            else
            {
                uint portfolioSize;
                if (!uint.TryParse(args[0], out portfolioSize))
                {
                    Console.WriteLine("Error parsing portfolioSize from:'{0}'", args[0]);
                    return;
                }

                if (portfolioSize < 2 || portfolioSize > 30)
                {
                    Console.WriteLine("Incorrect portfolio size, wait value at [2..30]");
                    return;
                }

                Process(portfolioSize);
            }
        }

        private static void Process(uint portfolioSize)
        {
            Console.WriteLine("Target yield (yield of MICEX30 index): {0}%", TARGET_YIELD);
            Console.WriteLine("Portfolio size: {0}", portfolioSize);
            Console.WriteLine("MICEX Index components: {0}", 30);
            Console.WriteLine("Total iterations: {0:n0}", Calculator.CombinationsCount(portfolioSize, 30));

            Console.WriteLine(TILDAS);
            Console.WriteLine(Calculator.FORMAT,
                "Iteration",
                "AboveTgt", "AboveTgt %",
                "BelowTgt", "BelowTgt %",
                "Complete, %");

            Console.WriteLine(TILDAS);

            var initTime = Stopwatch.StartNew();

            var calculator = new Calculator(TARGET_YIELD);
            initTime.Stop();

            var calcTime = Stopwatch.StartNew();
            calculator.Start(portfolioSize, 30);
            calcTime.Stop();

            Console.WriteLine(TILDAS);
            Console.WriteLine("Intialization E:{0}", initTime.Elapsed);
            Console.WriteLine("Calculation   E:{0}", calcTime.Elapsed);
            //Console.ReadLine();
        }
    }
}
