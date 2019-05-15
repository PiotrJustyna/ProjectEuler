using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem30
{
    class Program
    {
        private static Dictionary<int, int> _powers = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            _powers.Add(0, 0);
            _powers.Add(1, 1);

            List<int> correctNumbers = new List<int>();

            for (int i = 2; i <= 9; i++)
            {
                _powers.Add(i, (int)Math.Pow(i, 5));
            }

            Console.WriteLine("Available powers:");

            foreach (var singlePower in _powers)
            {
                Console.WriteLine($"{singlePower.Key}^5={singlePower.Value}");
            }

            Console.WriteLine("Combinations of two digits:");

            // This is the first iteration of the solution.
            // It works, but the "6" is hardcoded.
            // Thinking of a way to improve it.
            // ###
            // Edit:
            // Ah, it makes sense if you think of it:
            // if you form a number consisting of 5 nines (5 * (9 ^ 5)), you get 295,245 (6 digits)
            // going further:
            // 6 * (9 ^ 5) = 354,294 (6 digits)
            // 7 * (9 ^ 5) = 413,343 (still only 6 digits)
            // Result: 6 digits is the maximum length of a sum that can still match the length of the potential number.
            // From that point on, the length of potential numbers grows faster that the sums of even the highest available powers (9 ^ 5).
            // Result: 6 is a reasonable ceiling for the problem.
            var potentialNumbers = GenerateNumbers(_powers, 6);

            foreach (var singlePotentialNumber in potentialNumbers)
            {
                var sumOfPowers = singlePotentialNumber.Sum(x => x.Item2);
                var sumOfDecimalBaseDigits = singlePotentialNumber.Sum(x => x.Item3);

                if (sumOfPowers == sumOfDecimalBaseDigits)
                {
                    correctNumbers.Add(sumOfPowers);
                    Console.WriteLine($"{sumOfDecimalBaseDigits}:\t{sumOfPowers}");
                }
            }

            Console.WriteLine($"Sum of correct numbers: {correctNumbers.Sum(x => x) - 1}."); // "-1", because we're not interested in "1" and no need to subtract 0.
        }

        private static List<List<Tuple<int, int, int>>> GenerateNumbers(Dictionary<int, int> powers, int numberOfDigits)
        {
            List<List<Tuple<int, int, int>>> possibleDigits = new List<List<Tuple<int, int, int>>>();

            if (numberOfDigits == 1)
            {
                foreach (var singlePower in powers)
                {
                    possibleDigits.Add(new List<Tuple<int, int, int>>{
                        new Tuple<int, int, int>(
                            singlePower.Key,
                            singlePower.Value,
                            singlePower.Key)
                    });
                }

                return possibleDigits;
            }
            else
            {
                var previouslyGeneratedDigits = GenerateNumbers(powers, numberOfDigits - 1);

                foreach (var singleCollectionOfPreviouslyGeneratedDigits in previouslyGeneratedDigits)
                {
                    foreach (var singlePower in powers)
                    {
                        var singleNumber = new List<Tuple<int, int, int>>();
                        singleNumber.AddRange(singleCollectionOfPreviouslyGeneratedDigits);
                        singleNumber.Add(new Tuple<int, int, int>(
                            singlePower.Key,
                            singlePower.Value,
                            singlePower.Key * ((int)Math.Pow(10, numberOfDigits - 1))
                        ));

                        possibleDigits.Add(singleNumber);
                    }
                }

                return possibleDigits;
            }
        }
    }
}