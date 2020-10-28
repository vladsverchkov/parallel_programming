using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//Задано список заданого типу, а також декілька функцій, які перетворюють
//цей список(наприклад, алгоритми сортування). Застосувати кожну з функцій до початкового
//списку(незалежно, а не послідовно). Потім порівняти результати роботи функцій, повернути таблицю
//попарних відмінностей(чи однакові результати, якщо ні – чи відрізняються довжини списків, якщо відрізняються
//елементи – індекс першої відмінності).

namespace PP_Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<int> list1 = InitListNumbers(new List<int>(), 200);

            PrintList("Input list: ", list1);

            var task1 = Task.Factory.StartNew(() => BubbleSort(list1));
            var task2 = Task.Factory.StartNew(() => InsertionSorting(list1));
            var task3 = Task.Factory.StartNew(() => heapSort(list1, list1.Count));

            task1.Wait();
            task2.Wait();
            task3.Wait();

            PrintList("\nOutput list BUBBLE: ", task1.Result);
            PrintList("\nOutput list INSERTED_SORT: ", task2.Result);
            PrintList("\nOutput list HEAP_SORT: ", task3.Result);


            Console.ReadKey();
        }
        static List<int> InitListNumbers(List<int> list, int count)
        {
            var random = new Random();
            for (var i = 0; i < count; i++)
                list.Add(random.Next(9999));

            return list;
        }

        static void PrintList(string title, List<int> list)
        {
            Console.Write(title);


            foreach (var item in list)
                Console.Write(item + "  ");

            Console.WriteLine();
        }

        static List<int> BubbleSort(List<int> list)
        {
            int temp;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] > list[j])
                    {
                        temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }

            }

            Console.WriteLine("\nBubbleSort is finished (first algorithm)");
            return list;

        }

        static List<int> InsertionSorting(List<int> list)
        {
            int i, j, val, flag;
            for (i = 1; i < list.Count; i++)
            {
                val = list[i];
                flag = 0;
                for (j = i - 1; j >= 0 && flag != 1;)
                {
                    if (val < list[j])
                    {
                        list[j + 1] = list[j];
                        j--;
                        list[j + 1] = val;
                    }
                    else flag = 1;
                }
            }

            Console.WriteLine("\nInsertion Sort is finished (second algorithm)");
            return list;
        }

        static List<int> heapSort(List<int> list, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(list, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                int temp = list[0];
                list[0] = list[i];
                list[i] = temp;
                heapify(list, i, 0);
            }

            Console.WriteLine("\nHeapSort is finished (third algorithm)");

            return list;
        }
        static void heapify(List<int> list, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && list[left] > list[largest])
                largest = left;
            if (right < n && list[right] > list[largest])
                largest = right;
            if (largest != i)
            {
                int swap = list[i];
                list[i] = list[largest];
                list[largest] = swap;
                heapify(list, n, largest);
            }
        }



    }
}
