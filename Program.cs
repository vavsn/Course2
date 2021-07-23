using System;
using System.Diagnostics;

namespace CalcPath
{
    class Program
    {
        const int N = 5;
        const int M = 10;

        static void Print2(int n, int m, int[,] a)
        {
            int i, j;
            string format = "{0," + (a[n - 1, m - 1].ToString().Length + 1).ToString() + "}";
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                    Console.Write(string.Format(format, a[i, j]));
                Console.Write("\r\n");
            }
        }
        static void Print3(int n, int m, int[,] a)
        {
            int i, j;
            int maxLength = a[n - 1, m - 1].ToString().Length + 1;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                    Console.Write(string.Format("{0," + maxLength.ToString() + "}", a[i, j]));
                Console.Write("\r\n");
            }
        }

        static void Main(string[] args)
        {
            int[,] A = new int[N, M];
            int i, j;
            for (j = 0; j < M; j++)
                A[0, j] = 1; // Первая строка заполнена единицами
            for (i = 1; i < N; i++)
            {
                A[i, 0] = 1;
                for (j = 1; j < M; j++)
                    A[i, j] = A[i, j - 1] + A[i - 1, j];
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Print2(N, M, A);
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            stopWatch = Stopwatch.StartNew();
            Print3(N, M, A);
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);

            Console.WriteLine($"Количество путей из верхней левой клетки в правую нижнюю для поля {N}x{M} равно {A[N-1,M-1]}");
        }
    }
}
