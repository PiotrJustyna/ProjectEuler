using System;
using System.Collections.Generic;

namespace Problem34
{
    class Program
    {
        private static Dictionary<int, int> _factorials = new Dictionary<int, int>
        {
            // number, factorial
            { 0, 1 },
            { 1, 1 },
            { 2, 2 },
            { 3, 6 },
            { 4, 24 },
            { 5, 120 },
            { 6, 720 },
            { 7, 5040 },
            { 8, 40320 },
            { 9, 362880 }
        };

        static void Main(string[] args)
        {
            var sumOfFactorials = 0;
            var temp = 0;
            var lastDigit = 0;
            var answer = 0;

            for (int i = 10; i < _factorials[9] * 7; i++)
            {
                temp = i;

                while (temp > 0)
                {
                    lastDigit = temp % 10;
                    temp /= 10;

                    sumOfFactorials += _factorials[lastDigit];
                }

                temp = 0;

                if (i == sumOfFactorials)
                {
                    Console.WriteLine(i);

                    answer += i;
                }

                sumOfFactorials = 0;
            }

            Console.WriteLine($"Answer: {answer}.");
        }
    }
}
