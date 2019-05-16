using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem31
{
    class Program
    {
        private const int ValueOfOnePence = 1;

        private const int ValueOfTwoPence = 2;

        private const int ValueOfFivePence = 5;

        private const int ValueOfTenPence = 10;

        private const int ValueOfTwentyPence = 20;

        private const int ValueOfFiftyPence = 50;

        private const int ValueOfOnePound = 100;

        private const int ValueOfTwoPounds = 200;

        static void Main(string[] args)
        {
            var combinations = FindCoinCombinations(200);

            Console.WriteLine($"Number of different combinations found: {combinations.Count()}.");

            // foreach (var singleCombination in combinations)
            // {
            //     Console.WriteLine(singleCombination);
            // }
        }

        private static List<CombinationOfCoins> FindCoinCombinations(int sum)
        {
            List<CombinationOfCoins> coinCombinations = new List<CombinationOfCoins>();

            for (int numberOfOnePences = 0; numberOfOnePences <= sum / ValueOfOnePence; numberOfOnePences++)
            {
                for (int numberOfTwoPences = 0; numberOfTwoPences <= sum / ValueOfTwoPence; numberOfTwoPences++)
                {
                    for (int numberOfFivePences = 0; numberOfFivePences <= sum / ValueOfFivePence; numberOfFivePences++)
                    {
                        for (int numberOfTenPences = 0; numberOfTenPences <= sum / ValueOfTenPence; numberOfTenPences++)
                        {
                            for (int numberOfTwentyPences = 0; numberOfTwentyPences <= sum / ValueOfTwentyPence; numberOfTwentyPences++)
                            {
                                for (int numberOfFiftyPences = 0; numberOfFiftyPences <= sum / ValueOfFiftyPence; numberOfFiftyPences++)
                                {
                                    for (int numberOfOnePounds = 0; numberOfOnePounds <= sum / ValueOfOnePound; numberOfOnePounds++)
                                    {
                                        for (int numberOfTwoPounds = 0; numberOfTwoPounds <= sum / ValueOfTwoPounds; numberOfTwoPounds++)
                                        {
                                            if (numberOfOnePences * ValueOfOnePence +
                                                numberOfTwoPences * ValueOfTwoPence +
                                                numberOfFivePences * ValueOfFivePence +
                                                numberOfTenPences * ValueOfTenPence +
                                                numberOfTwentyPences * ValueOfTwentyPence +
                                                numberOfFiftyPences * ValueOfFiftyPence +
                                                numberOfOnePounds * ValueOfOnePound +
                                                numberOfTwoPounds * ValueOfTwoPounds == sum)
                                            {
                                                coinCombinations.Add(
                                                    new CombinationOfCoins(
                                                        numberOfOnePences,
                                                        numberOfTwoPences,
                                                        numberOfFivePences,
                                                        numberOfTenPences,
                                                        numberOfTwentyPences,
                                                        numberOfFiftyPences,
                                                        numberOfOnePounds,
                                                        numberOfTwoPounds));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return coinCombinations;
        }
    }
}
