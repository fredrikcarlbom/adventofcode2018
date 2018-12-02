using System;
using System.Linq;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            do {
                var token = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(token)) {
                    numbers.Add(Int32.Parse(token));
                } else {
                    break;
                }
            } while (true);
            Console.WriteLine("Sum:" + numbers.Sum());
            
            var sum = 0;
            var found = false;
            List<int> allSums = new List<int> { sum };
            do {
                foreach (var number in numbers) {
                    sum += number;
                    if (!allSums.Exists(item => item == sum)) {
                        Console.Write(sum + ", ");
                        allSums.Add(sum);
                    } else {
                        Console.WriteLine("BREAKING " + sum);
                        found = true;
                        break;
                    }
                }
            } while (!found);
            allSums.Sort();
            Console.WriteLine("All Sums: " + String.Join(", ",allSums));
            Console.WriteLine("First recurring: " + sum);
        }
    }
}
