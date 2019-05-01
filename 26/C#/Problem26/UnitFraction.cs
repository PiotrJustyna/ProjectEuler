using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem26
{
    public class UnitFraction
    {
        private readonly Dictionary<uint, IntegerDivisionResult> _allCarryoverDivisions;

        public UnitFraction(uint denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }

            Denominator = denominator;

            _allCarryoverDivisions = GetAllPossibleCarryoverDivisions();

            LoopLength = DetectLoop(_allCarryoverDivisions);
        }

        public uint Denominator { get; }

        public uint LoopLength { get; }

        public override String ToString()
        {
            Boolean debug = false;

            if(debug)
            {
                var printableAllDivisions = new StringBuilder();

                foreach(var singleDivision in _allCarryoverDivisions)
                {
                    printableAllDivisions.AppendLine($"{singleDivision.Key}.\t{singleDivision.Key * 10}/{Denominator} = integer division result: {singleDivision.Value.Quotient}, integer division remainder: {singleDivision.Value.Remainder}");
                }

                if(_allCarryoverDivisions.Count > 0)
                {
                    return $"{1}/{Denominator}\t=\t{(1.0 / Denominator)}\nAll carryover divisions:\n{printableAllDivisions.ToString()}. Loop length: {LoopLength}.";
                }
                else
                {
                    return $"1/{Denominator}\t=\t{(1.0 / Denominator)}. Loop length: {LoopLength}.";
                }
            }
            else
            {
                return $"{1}/{Denominator}\t=\t{(1.0 / Denominator)}. Loop length: {LoopLength}.";
            }
        }

        private uint DetectLoop(Dictionary<uint, IntegerDivisionResult> allDivisions)
        {
            var visitedDivisions = new List<IntegerDivisionResult>();
            var currentDivision = allDivisions.Any() ? allDivisions[1] : null;
            var noLoop = currentDivision == null ? true : false;

            while (
                currentDivision != null &&
                !visitedDivisions.Any(x =>
                    x.Quotient == currentDivision.Quotient &&
                    x.Remainder == currentDivision.Remainder) &&
                !noLoop)
            {
                if(currentDivision.Remainder == 0)
                {
                    // no remainder => no loop
                    noLoop = true;
                }
                else
                {
                    visitedDivisions.Add(
                        new IntegerDivisionResult(
                            quotient: currentDivision.Quotient,
                            remainder: currentDivision.Remainder));

                    currentDivision = allDivisions[currentDivision.Remainder];
                }
            }

            if(noLoop)
            {
                return 0;
            }
            else
            {
                return (uint)(visitedDivisions.Count -
                    visitedDivisions.IndexOf(
                        visitedDivisions.First(x =>
                            x.Quotient == currentDivision.Quotient &&
                            x.Remainder == currentDivision.Remainder)));
            }
        }

        private Dictionary<uint, IntegerDivisionResult> GetAllPossibleCarryoverDivisions()
        {
            var result = new Dictionary<uint, IntegerDivisionResult>();

            uint carryoverNominator = 0;

            for (uint nominator = 1; nominator < Denominator; nominator++)
            {
                carryoverNominator = nominator * 10;

                result.Add(
                    key: nominator,
                    value: new IntegerDivisionResult(
                        quotient: carryoverNominator / Denominator,
                        remainder: carryoverNominator % Denominator));
            }

            return result;
        }
    }
}
