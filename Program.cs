using System;

namespace Task1
{
    class Program
    {
        /// <summary>
        /// класс для проведения тестирования
        /// </summary>
        public class TestCase
        {
            public int X { get; set; }
            public string Expected { get; set; }
            public Exception ExpectedException { get; set; }
        }

        /// <summary>
        /// метод проведения теста корректности работы процедуры PrimeNumber
        /// </summary>
        /// <param name="testCase"></param>
        static void TestPrimeNumber(TestCase testCase)
        {
            try
            {
                var actual = PrimeNumber(testCase.X);

                if (string.Compare( actual, testCase.Expected) == 0)
                {
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
            catch (Exception ex)
            {
                if (testCase.ExpectedException != null)
                {
                    //TODO add type exception tests;
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
        }

        /// <summary>
        /// строковое представление результатов работы процедуры PrimeNumber
        /// </summary>
        private static string NotPrime = "НЕ ПРОСТОЕ";
        private static string Prime = "ПРОСТОЕ";

        /// <summary>
        /// метод определения простое число введено пользователем или нет
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static string PrimeNumber(int num)
        {
            string resume = NotPrime;
            int d = 0;
            int i = 2;
            while (i < num)
            {
                if (num % i == 0)
                    d++;
                i++;
            }

            if (d == 0)
                resume = Prime;

            return resume;
        }

    static void Main(string[] args)
        {
            Console.WriteLine("Программа проверки простое число или нет.");
            Console.WriteLine("Введите целое число: ");
            int askNum = 0;
            bool success = Int32.TryParse(Console.ReadLine(), out askNum);
            // проверяемое число должно быть целым, иначе генерируем исключение
            if (!success)
            {
                throw new ArgumentException("Не является целым");
            }

            // проводим проверку
            string checkNumber = PrimeNumber(askNum);

            Console.WriteLine($"Введено {checkNumber} число {askNum} ");

            // проводим тесты корректности работы процедуры PrimeNumber
            var testCase = new TestCase()
            {
                X = 4,
                Expected = NotPrime,
                ExpectedException = null
            };

            TestPrimeNumber(testCase);

            testCase = new TestCase()
            {
                X = 6,
                Expected = Prime,
                ExpectedException = null
            };

            TestPrimeNumber(testCase);

            testCase = new TestCase()
            {
                X = 3,
                Expected = Prime,
                ExpectedException = null
            };

            TestPrimeNumber(testCase);

            testCase = new TestCase()
            {
                X = 823,
                Expected = NotPrime,
                ExpectedException = null
            };

            TestPrimeNumber(testCase);

            testCase = new TestCase()
            {
                X = 823,
                Expected = Prime,
                ExpectedException = null
            };

            TestPrimeNumber(testCase);
        }
    }
}
