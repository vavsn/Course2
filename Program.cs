using System;
using System.Collections.Generic;

namespace BucketSort
{

    class Program
    {
        /// <summary>
        /// метод сортировки массива
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static List<int> Sort(int[] x)
        {
            List<int> sortedArray = new List<int>();

            int numBuck = 10;

            List<int>[] buckets = new List<int>[numBuck];
            for (int i = 0; i < numBuck; i++)
            {
                buckets[i] = new List<int>();
            }

            for (int i = 0; i < x.Length; i++)
            {
                int bucket = (x[i] / numBuck);
                buckets[bucket].Add(x[i]);
            }

            for (int i = 0; i < numBuck; i++)
            {
                List<int> temp = SortBucket(buckets[i]);
                sortedArray.AddRange(temp);
            }
            return sortedArray;
        }

        /// <summary>
        /// метод сортирует содержимое корзины
        /// </summary>
        /// <param name="inp"></param>
        /// <returns></returns>
        public static List<int> SortBucket(List<int> inp)
        {
            for (int i = 1; i < inp.Count; i++)
            {
                int curValue = inp[i];
                int pnt = i - 1;

                while (pnt >= 0)
                {
                    if (curValue < inp[pnt])
                    {
                        inp[pnt + 1] = inp[pnt];
                        inp[pnt] = curValue;
                    }
                    else break;
                }
            }

            return inp;
        }

        static void Main(string[] args)
        {
            int[] array = new int[] { 6, 56, 84, 12, 3, 11, 29, 55, 49, 21, 77, 92, 81, 14, 66 };
            
            Console.WriteLine("Сорировка методом \"Bucket Sort\"");
            Console.WriteLine("Массив чисел ДО сортировки");

            foreach (var a in array)
            {
                Console.Write(" " + a.ToString());
            }

            List<int> sortedList = Sort(array);

            Console.WriteLine();
            Console.WriteLine("Результаты сортировки");
            foreach (var a in sortedList)
            {
                Console.Write(" " + a.ToString());
            }

        }
    }
}