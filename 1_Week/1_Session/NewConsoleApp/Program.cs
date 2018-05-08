using System;

namespace NewConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbys = new int[]
            {
                234,2345,23,13,13445,2345,1,-124
            };

            int smallestNumby = MinNum(numbys);

            Console.WriteLine(smallestNumby);
            
        }

        static int MinNum(int[] numbers)
        {
            // find the lowest one!
            int currMin = numbers[0];

            // iterate through the array
            for(int i = 0; i<numbers.Length; i++)
            {
                if(currMin > numbers[i])
                    currMin = numbers[i];
            }
            return currMin;
        }
    }
}
