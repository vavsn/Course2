using System;

namespace Task2
{

    class Program
    {

        /// <summary>
        /// класс для проведения тестирования
        /// </summary>
        public class TestCase
        {
            public ulong X { get; set; }
            public ulong Expected { get; set; }
            public Exception ExpectedException { get; set; }
        }


        static void TestNumberR(TestCase testCase)
        {
            try
            {
                ulong actual = FibRec(testCase.X);

                if (actual == testCase.Expected)
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

        static void TestNumberF(TestCase testCase)
        {
            try
            {
                ulong actual = FibFor(testCase.X);

                if (actual == testCase.Expected)
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
        /// метод рекурсивного вычисления числа Фибоначчи
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static ulong calcRec(ulong p1, ulong p2, ulong n)
        {
            return n == 0 ? p1 : calcRec(p2, p1 + p2, n - 1);
        }
        static ulong FibRec(ulong n) { return calcRec(0, 1, n); }
        static ulong calcFor(ulong p1, ulong p2, ulong n)
        //int calcFor(int n, int p1 = 0, int p2 = 1)
        {
            if (n <= 1) return p1;
            ulong p;
            /*
            for (ulong j = 2; j < n; j++)
            {
                p = p1;
                p1 = p2;
                p2 = p2 + p;
            }/**/

            while (n > 0)
            {
                p = p1;
                p1 = p2;
                p2 = p2 + p;
                n--;
            }
            return p1;
        }
        static ulong FibFor(ulong n) { return calcFor(0, 1, n); }

        static void Main(string[] args)
        {
            Console.WriteLine("Программа проверки простое число или нет.");
            Console.WriteLine("Введите целое число: ");
            ulong askNum = 0;
            bool success = ulong.TryParse(Console.ReadLine(), out askNum);
            if (!success)
            {
                throw new ArgumentException("Не является целым");
            }

            Console.WriteLine($"Введено число {askNum} ");

            Console.WriteLine($"Число Фибоначчи = {FibRec(askNum)} (вычислено рекурсивно)");
            Console.WriteLine($"Число Фибоначчи = {FibFor(askNum)} (вычислено в цикле)");

            Console.WriteLine("Проведение тестов вычисления числа Фибоначчи рекурсивным методом");

            var testCase = new TestCase()
            {
                X = 4,
                Expected = 3,
                ExpectedException = null
            };

            TestNumberR(testCase);

            testCase = new TestCase()
            {
                X = 7,
                Expected = 13,
                ExpectedException = null
            };

            TestNumberR(testCase);

            testCase = new TestCase()
            {
                X = 18,
                Expected = 2584,
                ExpectedException = null
            };

            TestNumberR(testCase);

            testCase = new TestCase()
            {
                X = 4,
                Expected = 4,
                ExpectedException = null
            };

            TestNumberR(testCase);

            testCase = new TestCase()
            {
                X = 18,
                Expected = 2565,
                ExpectedException = null
            };

            TestNumberR(testCase);

            Console.WriteLine("Проведение тестов вычисления числа Фибоначчи с помощью цикла");

            testCase = new TestCase()
            {
                X = 4,
                Expected = 3,
                ExpectedException = null
            };

            TestNumberF(testCase);

            testCase = new TestCase()
            {
                X = 7,
                Expected = 13,
                ExpectedException = null
            };

            TestNumberF(testCase);

            testCase = new TestCase()
            {
                X = 18,
                Expected = 2584,
                ExpectedException = null
            };

            TestNumberF(testCase);

            testCase = new TestCase()
            {
                X = 4,
                Expected = 4,
                ExpectedException = null
            };

            TestNumberF(testCase);

            testCase = new TestCase()
            {
                X = 18,
                Expected = 2565,
                ExpectedException = null
            };

            TestNumberF(testCase);
            /**/
        }
    }
}
