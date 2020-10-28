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



    }
}
