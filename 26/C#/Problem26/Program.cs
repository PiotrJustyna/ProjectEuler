using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem26
{
    class Program
    {
        static void Main(string[] args)
        {
            var fractions = new List<UnitFraction>();

            for(UInt16 i = 1; i <= 1000; i++)
            {
                var fraction = new UnitFraction(i);

                fractions.Add(fraction);

                Console.WriteLine(fraction);
            }

            var longestLoop  = fractions.OrderByDescending(x => x.LoopLength).First();

            Console.WriteLine($"The longest loop is for 1/{longestLoop.Denominator}: {longestLoop.LoopLength}.");
        }
    }
}
