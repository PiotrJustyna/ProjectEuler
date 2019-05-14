using System;
using System.Collections.Generic;

namespace Problem29
{
    class Program
    {
        private const int MinA = 2;

        private const int MaxA = 100;

        private const int MinB = 2;

        private const int MaxB = 100;

        private static Dictionary<Double, Tuple<int, int>> _distinctTerms = new Dictionary<double, Tuple<int, int>>(); 

        static void Main(string[] args)
        {
            for (int a = MinA; a <= MaxA; a++)
            {
                for (int b = MinB; b <= MaxB; b++)
                {
                    var power = Math.Pow(a, b);

                    if(!_distinctTerms.ContainsKey(power))
                    {
                        _distinctTerms.Add(power, new Tuple<int, int>(a, b));
                    }
                }
            }

            Console.WriteLine($"There are {_distinctTerms.Count} distinct terms.");
        }
    }
}
