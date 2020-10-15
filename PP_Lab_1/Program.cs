using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void InitRandomListData(ref List<int> numbers, int count)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
           
            var random = new Random();

            for (var i = 0; i < count; i++)
                numbers.Add(random.Next(99999));
          
            watch.Stop();

            Console.WriteLine($"InitRandomListData => Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        static void InitRandomNumbersAndSaveToFile(int count)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            using (var writer = File.AppendText("numbers.dat"))
            {
                var random = new Random();

                for (var i = 0; i < count; i++)
                    writer.WriteLine(random.Next(99999));

                writer.Close();
            }

            watch.Stop();

            Console.WriteLine($"InitRandomNumbersAndSaveToFile => Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        static void SieveEratosthenes(int number)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var numbers = new List<int>();

            for (var i = 2; i < number; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2; j < number; j++)
                {
                    numbers.Remove(numbers[i] * j);
                }
            }

            watch.Stop();

            Console.WriteLine($"SieveEratosthenes => Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
