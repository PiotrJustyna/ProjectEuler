using System;

namespace Problem28
{
    class Program
    {
        private const int problemSideLength = 1001;

        private const int numberOfRings = ((problemSideLength - 1) / 2);

        private static int sum = 1;

        static void Main(string[] args)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            int fourth = 0;
            int lastFourth = 1;
            int sideLength = 3;
            int step = sideLength - 1;

            for (int i = 1;  i <= numberOfRings; i++)
            {
                first = lastFourth + step;
                second = first + step;
                third = second + step;
                fourth = third + step;
                lastFourth = fourth;
                sum += first + second + third + fourth;
                sideLength += 2;
                step = sideLength - 1;
            }

            Console.WriteLine($"Sum: {sum}.");
        }
    }
}
