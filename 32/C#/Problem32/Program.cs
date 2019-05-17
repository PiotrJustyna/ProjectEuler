using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem32
{
    class Program
    {
        static void Main(string[] args)
        {
            uint counter = 1;

            var uniqueGroups = new Dictionary<(byte, byte), byte>();

            var numbersWithUniqueDigits = new Dictionary<byte, Dictionary<ushort, HashSet<byte>>>();

            DateTime start = DateTime.Now;

            #region Step 1: Generating potential groups of numbers

            Console.WriteLine("Generating potential groups of numbers:");

            for (byte groupMultiplicandLength = 1; groupMultiplicandLength <= 7; groupMultiplicandLength++)
            {
                for (byte groupMultiplierLength = 1; groupMultiplierLength <= 7; groupMultiplierLength++)
                {
                    for (byte groupProductLength = 1; groupProductLength <= 7; groupProductLength++)
                    {
                        if ((groupMultiplicandLength + groupMultiplierLength > 4) && // Only groups whose lengths sum up to 5 or more have a chance of resulting in products of the desired length.
                            groupMultiplicandLength + groupMultiplierLength + groupProductLength == 9 &&
                            groupProductLength >= (groupMultiplicandLength > groupMultiplierLength ? groupMultiplicandLength : groupMultiplierLength))

                        {
                            if (!uniqueGroups.ContainsKey((groupMultiplierLength, groupMultiplicandLength)))
                            {
                                uniqueGroups.Add((groupMultiplicandLength, groupMultiplierLength), groupProductLength);

                                Console.WriteLine($"{counter++}.\tPotential group layout: ({groupMultiplicandLength} digits) * ({groupMultiplierLength} digits) = ({groupProductLength} digits)");
                            }
                        }
                    }
                }
            }

            #endregion // Step 1

            #region Step 2: Generating numbers with unique digits to fit the found groups

            Console.WriteLine("Generating numbers with unique digits to fit the found groups:");

            counter = 1;

            foreach (var singleGroup in uniqueGroups)
            {
                if (!numbersWithUniqueDigits.ContainsKey(singleGroup.Key.Item1))
                {
                    numbersWithUniqueDigits.Add(singleGroup.Key.Item1, GenerateNumbersWithUniqueDigitsOfGivenLength(singleGroup.Key.Item1));

                    Console.WriteLine($"{counter++}.\tNumber of numbers of length {singleGroup.Key.Item1}: {numbersWithUniqueDigits[singleGroup.Key.Item1].Count}.");
                }

                if (!numbersWithUniqueDigits.ContainsKey(singleGroup.Key.Item2))
                {
                    numbersWithUniqueDigits.Add(singleGroup.Key.Item2, GenerateNumbersWithUniqueDigitsOfGivenLength(singleGroup.Key.Item2));

                    Console.WriteLine($"{counter++}.\tNumber of numbers of length {singleGroup.Key.Item2}: {numbersWithUniqueDigits[singleGroup.Key.Item2].Count}.");
                }

                if (!numbersWithUniqueDigits.ContainsKey(singleGroup.Value))
                {
                    numbersWithUniqueDigits.Add(singleGroup.Value, GenerateNumbersWithUniqueDigitsOfGivenLength(singleGroup.Value));

                    Console.WriteLine($"{counter++}.\tNumber of numbers of length {singleGroup.Value}: {numbersWithUniqueDigits[singleGroup.Value].Count}.");
                }
            }

            #endregion // Step 2

            #region Step 3: Generating products

            Console.WriteLine("Generating products:");

            counter = 1;

            var resultingProducts = new List<(ushort, ushort, ushort)>();

            foreach (var singleGroup in uniqueGroups)
            {
                resultingProducts.AddRange(
                    CalculateProducts(
                        possibleMultiplicands: numbersWithUniqueDigits[singleGroup.Key.Item1],
                        possibleMultipliers: numbersWithUniqueDigits[singleGroup.Key.Item2],
                        productLength: singleGroup.Value));
            }

            foreach (var singleProduct in resultingProducts)
            {
                Console.WriteLine($"{counter++}.\t{singleProduct.Item1} * {singleProduct.Item2} = {singleProduct.Item3}");
            }

            Console.WriteLine($"Answer for distinct products: {resultingProducts.Select(x => x.Item3).Distinct().Sum(x => x)}. Time taken: {(DateTime.Now - start).Milliseconds}ms.");

            #endregion // Step 3
        }

        private static Dictionary<ushort, HashSet<byte>> GenerateNumbersWithUniqueDigitsOfGivenLength(byte length)
        {
            var result = new Dictionary<ushort, HashSet<byte>>();

            if (length == 1)
            {
                for (ushort i = 1; i <= 9; i++)
                {
                    result.Add(i, new HashSet<byte> { (byte)i });   // This cast is safe as we're only dealing with single digit numbers.
                }
            }
            else
            {
                foreach (var singleNumber in GenerateNumbersWithUniqueDigitsOfGivenLength((byte)(length - 1)))
                {
                    for (ushort i = 1; i <= 9; i++)
                    {
                        if (!singleNumber.Value.Contains((byte)i))   // This cast is safe as we're only dealing with single digit numbers.
                        {
                            var newValue = new HashSet<byte>(singleNumber.Value);
                            newValue.Add((byte)i);                  // This cast is safe as we're only dealing with single digit numbers.

                            result.Add(
                                key: (ushort)(singleNumber.Key + (i * Math.Pow(10, (length - 1)))),
                                value: newValue);
                        }
                    }
                }
            }

            return result;
        }

        private static List<(ushort, ushort, ushort)> CalculateProducts(
            Dictionary<ushort, HashSet<byte>> possibleMultiplicands,
            Dictionary<ushort, HashSet<byte>> possibleMultipliers,
            byte productLength)
        {
            var result = new List<(ushort, ushort, ushort)>();

            foreach (var singlePossibleMultiplicand in possibleMultiplicands)
            {
                foreach (var singlePossibleMultiplier in possibleMultipliers)
                {
                    if (!AreDigitsReused(singlePossibleMultiplicand.Value, singlePossibleMultiplier.Value))
                    {
                        // Using "int" is not an omission here, it is just more convenient that way.
                        // It will be cased to ushort once we do the checks below.
                        int product = (singlePossibleMultiplicand.Key * singlePossibleMultiplier.Key);

                        if (product < Math.Pow(10, productLength) &&
                            product >= Math.Pow(10, productLength - 1))
                        {
                            var digits = ExtractProductDigits((ushort)product);

                            var productContainsZeros = digits.Any(x => x == 0);

                            var productDegitsReused = AreProductDigitsReused(
                                firstNumber: singlePossibleMultiplicand.Value,
                                secondNumber: singlePossibleMultiplier.Value,
                                productDigits: digits);

                            if(!productContainsZeros && !productDegitsReused)
                            {
                                result.Add((singlePossibleMultiplicand.Key, singlePossibleMultiplier.Key, (ushort)product));
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static bool AreDigitsReused(HashSet<byte> firstNumber, HashSet<byte> secondNumber)
        {
            bool digitsReused = false;

            for (byte i = 1; i <= 9 && !digitsReused; i++)
            {
                digitsReused = firstNumber.Contains(i) && secondNumber.Contains(i);
            }

            return digitsReused;
        }

        private static List<byte> ExtractProductDigits(ushort product)
        {
            var productDigits = new List<byte>();

            ushort quotient = product;

            byte remainder = 0;

            while(quotient > 0)
            {
                quotient = (ushort)(product / 10);

                remainder = (byte)(product % 10);

                product = quotient;

                productDigits.Add(remainder);
            }

            return productDigits;
        }

        private static bool AreProductDigitsReused(
            HashSet<byte> firstNumber,
            HashSet<byte> secondNumber,
            List<byte> productDigits)
        {
            bool reusedDigitsFound = productDigits.Distinct().Count() != productDigits.Count;

            if(!reusedDigitsFound)
            {
                for (byte i = 0; i < productDigits.Count && !reusedDigitsFound; i++)
                {
                    byte singleDigit = productDigits[i];
                    
                    reusedDigitsFound = firstNumber.Contains(singleDigit) || secondNumber.Contains(singleDigit);
                }
            }

            return reusedDigitsFound;
        }
    }
}
