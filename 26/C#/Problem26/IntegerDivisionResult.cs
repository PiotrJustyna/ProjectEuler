using System;

namespace Problem26
{
    public class IntegerDivisionResult
    {
        public IntegerDivisionResult(
            uint quotient,
            uint remainder)
        {
            Quotient = quotient;
            Remainder = remainder;
        }

        public uint Quotient { get; }
        public uint Remainder { get; }
    }
}