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
            var numbers = new List<int>();
            var thread = new List<Thread>();

            Console.Write(
                "Select option:\n" +
                "1 - Multithreading\n" +
                "2 - No Multithreading\n>");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    // Мультипоточність

                    thread.Add(new Thread(() => InitRandomNumbersAndSaveToFile(9999999)));
                    thread.Add(new Thread(() => InitRandomListData(ref numbers, 99999999)));                 
                    thread.Add(new Thread(() => SieveEratosthenes(1900)));
                 
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    //Запускаємо потоки
                    foreach (Thread t in thread)
                        t.Start();
                    //Закриваємо потоки після того, як кожен з них відпрацював своє
                    foreach (Thread t in thread)
                        t.Join();

                    watch.Stop();

                    Console.WriteLine($"Multithreading time => Execution Time: {watch.ElapsedMilliseconds} ms");                 

                    break;
                case 2:

                    // Послідовна обробка функцій
                    var watch_n = System.Diagnostics.Stopwatch.StartNew();

                    InitRandomNumbersAndSaveToFile(9999999);
                    InitRandomListData(ref numbers, 99999999);
                    SieveEratosthenes(1900);

                    watch_n.Stop();

                    Console.WriteLine($"Fixed queue time => Execution Time: {watch_n.ElapsedMilliseconds} ms");
                    break;
            }

            Console.ReadKey();
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
