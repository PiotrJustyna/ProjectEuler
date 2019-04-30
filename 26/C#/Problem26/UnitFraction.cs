using System;
using System.Collections.Generic;
using System.Text;

namespace Problem26
{
    public class UnitFraction
    {
        public UnitFraction(UInt16 denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }

            Denominator = denominator;
        }

        public UInt16 Denominator { get; }

        public override String ToString()
        {
            var printableDecimalFractionPartRepresentation = new StringBuilder();
            LinkedList<UInt16> decimalFractionPartRepresentation = GetDecimalFractionPartRepresentation();

            foreach (UInt16 singleDigit in decimalFractionPartRepresentation)
            {
                printableDecimalFractionPartRepresentation.Append(singleDigit);
            }

            return $"1/{Denominator}\t=\t{(1.0 / Denominator):0.##}\taka\t{printableDecimalFractionPartRepresentation.ToString()},\tcycle length:\t{FindCycle(decimalFractionPartRepresentation)}";
        }

        private LinkedList<UInt16> GetDecimalFractionPartRepresentation()
        {
            UInt16 currentNominator = 1;
            UInt16 integerDivision = 0;
            UInt16 modulo = 0;
            var result = new LinkedList<UInt16>();

            for (Byte i = 0; i < 100; i++)
            {
                integerDivision = (UInt16)(currentNominator / Denominator);
                modulo = (UInt16)(currentNominator % Denominator);
                currentNominator = (UInt16)(modulo * 10);
                result.AddLast(integerDivision);
            }

            return result;
        }

        private UInt16 FindCycle(LinkedList<UInt16> collection)
        {
            LinkedListNode<UInt16> tortoise = collection.First;
            LinkedListNode<UInt16> hare = collection.First;
            UInt16 loopLength = 0;
            Boolean loopFound = false;
            Boolean endReached = false;

            while (!loopFound && !endReached)
            {
                if (hare != collection.Last)
                {
                    hare = hare.Next;

                    if (hare != collection.Last)
                    {
                        hare = hare.Next;
                        tortoise = tortoise.Next;

                        if (hare.Value == tortoise.Value)
                        {
                            loopFound = true;

                            // now let the tortoise catch up
                            // to get the length of the loop
                            tortoise = tortoise.Next;
                            loopLength++;

                            while (hare.Value != tortoise.Value)
                            {
                                tortoise = tortoise.Next;
                                loopLength++;
                            }
                        }
                    }
                    else
                    {
                        endReached = true;
                    }
                }
                else
                {
                    endReached = true;
                }
            }

            return loopLength;
        }
    }
}

