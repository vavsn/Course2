using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace Benchmark
{
    public class Program
    {

        public class TheBenchmark
        {
            /// <summary>
            /// класс хранения информации по точке в пространстве
            /// </summary>
            public class PointClass
            {
                public float X;
                public float Y;
            }

            /// <summary>
            /// структура хранения информации по точке в пространстве
            /// </summary>
            public struct PointStruct
            {
                public float X;
                public float Y;
            }

            public static PointClass[] p1 = DistClass; // первый набор точек ссылочного типа
            public static PointClass[] p2 = DistClass; // второй набор точек ссылочного типа
            public static PointStruct[] ps1 = DistStruct; // первый набор точек значимого типа
            public static PointStruct[] ps2 = DistStruct; // первый набор точек значимого типа

            /// <summary>
            /// генерация массива дистанций для расчетов
            /// </summary>
            public static PointClass[] DistClass
            {
                get
                {
                    PointClass[] d = new PointClass[100];
                    Random rnd = new Random();
                    int l = d.Length;
                    for (int i = 0; i < l; i++)
                    {
                        d[i] = new PointClass();
                        d[i].X = (float)rnd.NextDouble() * 100;
                        d[i].Y = (float)rnd.NextDouble() * 100;
                    }
                    return d;
                }
            }
            public static PointStruct[] DistStruct
            {
                get
                {
                    PointStruct[] d = new PointStruct[100];
                    Random rnd = new Random();
                    int l = d.Length;
                    for (int i = 0; i < l; i++)
                    {
                        d[i] = new PointStruct();
                        d[i].X = (float)rnd.NextDouble() * 100;
                        d[i].Y = (float)rnd.NextDouble() * 100;
                    }
                    return d;
                }
            }

            /// <summary>
            /// функции расчета дистанции между точками
            /// </summary>
            /// <param name="pointOne"></param>
            /// <param name="pointTwo"></param>
            /// <returns></returns>
            public float PointDistance(PointStruct pointOne, PointStruct pointTwo)
            {
                float x = pointOne.X - pointTwo.X;
                float y = pointOne.Y - pointTwo.Y;
                return MathF.Sqrt((x * x) + (y * y));
            }
            public float PointDistance(PointClass pointOne, PointClass pointTwo)
            {
                float x = pointOne.X - pointTwo.X;
                float y = pointOne.Y - pointTwo.Y;
                return MathF.Sqrt((x * x) + (y * y));
            }
            public float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
            {
                float x = pointOne.X - pointTwo.X;
                float y = pointOne.Y - pointTwo.Y;
                return (x * x) + (y * y);
            }

            /// <summary>
            /// методы проведения замеров производительности
            /// </summary>
            /// <returns></returns>
            [Benchmark(Description = "Raschet1")]
            public float Ras1()
            {
                // Обычный метод расчёта дистанции со ссылочным типом (PointClass — координаты типа float).
                float res = 0;
                int l = p1.Length;
                for (int i = 0; i < l; i++)
                {
                    res += PointDistance(p1[i], p2[i]);
                }
                return res;
            }
            [Benchmark(Description = "Raschet2")]
            public float Ras2()
            {
                // Обычный метод расчёта дистанции со ссылочным типом (PointClass — координаты типа float).
                float res = 0;
                int l = p1.Length;
                for (int i = 0; i < l; i++)
                {
                    res += PointDistance(ps1[i], ps2[i]);
                }
                return res;
            }
            [Benchmark(Description = "Raschet3")]
            public double Ras3()
            {
                // Обычный метод расчёта дистанции со значимым типом (PointStruct — координаты типа double).
                double res = 0;
                int l = p1.Length;
                for (int i = 0; i < l; i++)
                {
                    res += PointDistance(ps1[i], ps2[i]);
                }
                return res;
            }
            [Benchmark(Description = "Raschet4")]
            public float Ras4()
            {
                // Метод расчёта дистанции без квадратного корня со значимым типом (PointStruct — координаты типа float)
                float res = 0;
                int l = p1.Length;
                for (int i = 0; i < l; i++)
                {
                    res += PointDistanceShort(ps1[i], ps2[i]);
                }
                return res;
            }

        }
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TheBenchmark>();
        }
    }
}
