using System;

namespace BinarySearch
{
    class Program
    {
        /// <summary>
        /// метод бинарного поиска значаения
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid])
                {
                    return mid;
                }
                else if (searchValue < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            // заполнение первоначального массива
            int[] mass1 = new int[100];
            for (int i = 0; i < 100; i++)
                mass1[i] = i;
            for (int i = 0; i < 100; i++)
                Console.WriteLine("{0} {1}", i, mass1[i]);

            // задаём искомое значение
            int srch = 3;

            // производим поиск
            int resultSearch = BinarySearch(mass1, srch);

            // вывод резальтатов поиска
            if (resultSearch != -1)
                Console.WriteLine($"Искали значение {resultSearch}");
            else
                Console.WriteLine($"Искомое значение {srch} не найдено");

            Console.ReadKey();
        }
    }
}
