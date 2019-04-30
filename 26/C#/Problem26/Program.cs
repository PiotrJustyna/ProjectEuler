using System;

namespace Problem26
{
    class Program
    {
        static void Main(string[] args)
        {
            for(UInt16 i = 1; i <= 300; i++)
            {
                Console.WriteLine(new UnitFraction(i));
            }
        }
    }
}
