using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem33
{
    /*
    This problem is formulated in a way that just left me confused.
    I did some research a found this brilliant blog: https://www.xarg.org/puzzle/project-euler/problem-33/
    Which made the problem really clear.

    For those of you who are not clear on it either, the challenge is to find such nominators and denominators where:

    * denominator > nominator (value of the fraction is < 1)
    * nominator and denominator are represented by two digits each (ab / cd)
    * the fraction is positive
    * b = c
    * ab / bc = a / c

    The author of the linked blog comes up with four equations that would solve the problem,
    but I feel the solution could be presented in a simpler way. Here's my take on it.
    What we are really after is only one equation and given ab / bc = a / c, we get the following steps:

    * (10a + b) / (10b + c) = a / c
    * 10a + b = (10ab + ac) / c
    * 10ac + bc = 10ab + ac
    * 9ac = 10ab - bc

    We could solve this by brute force already in 729 iterations (9 * 9 * 9), which is already more than good enough. See solution 1.

    However, we can take it one step further a limit the range of a, b and c by transforming the last equation as follows:

    * 9ac = 9ab + ab - bc
    * 9ac - 9ab = ab -bc
    * 9a * (c - b) = b * (a - c)

    Now, since we know that a < c (for the fraction to be < 1), the right hand side becomes negative.
    Result: left hand side also has to be negative. Therefore: b > c.
    Now, we know this: 1 <= a < c < b <= 9. That lets us formulate solution 2, which is just 84 iterations, which is a very nice improvement.

    */
    class Program
    {
        static void Main(string[] args)
        {
            var results = Solution2();
            ushort finalNominator = 1;
            ushort finalDenominator = 1;

            Console.WriteLine("Found fractions:");

            foreach (var singleResult in results)
            {
                Console.WriteLine($"{singleResult.Item1}{singleResult.Item2} / {singleResult.Item2}{singleResult.Item3} = {singleResult.Item1} / {singleResult.Item3}");

                finalNominator *= singleResult.Item1;
                finalDenominator *= singleResult.Item3;
            }

            var gcd = GCD(finalDenominator, finalNominator);
            var reducedFinalNominator = finalNominator / gcd;
            var reducedFinalDenominator = finalDenominator / gcd;

            Console.WriteLine($"Resulting fraction: {finalNominator}/{finalDenominator}.");
            Console.WriteLine($"Reduced resulting fraction: {reducedFinalNominator}/{reducedFinalDenominator}.");
            Console.WriteLine($"Answer: {reducedFinalDenominator}.");
        }

        private static ushort GCD(ushort a, ushort b)
        {
            ushort temp = b;

            while (b != 0)
            {
                temp = b;
                b = (ushort)(a % b);
                a = temp;
            }

            return a;
        }

        private static List<(byte, byte, byte)> Solution1()
        {
            var results = new List<(byte, byte, byte)>();

            for (byte a = 1; a <= 9; a++)
            {
                for (byte b = 1; b <= 9; b++)
                {
                    for (byte c = 1; c <= 9; c++)
                    {
                        if (!(a == b && a == c) &&
                            9 * a * c == (10 * a * b) - (b * c))
                        {
                            results.Add((a, b, c));
                        }
                    }
                }
            }

            return results;
        }

        private static List<(byte, byte, byte)> Solution2()
        {
            var results = new List<(byte, byte, byte)>();

            for (byte b = 1; b <= 9; b++)
            {
                for (byte c = 1; c < b; c++)
                {
                    for (byte a = 1; a < c; a++)
                    {
                        if (9 * a * c == (10 * a * b) - (b * c))
                        {
                            results.Add((a, b, c));
                        }
                    }
                }
            }

            return results;
        }
    }
}
