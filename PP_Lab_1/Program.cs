﻿using System;
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

       
    }
}