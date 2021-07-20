using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graph
{
    public class Graph
    {
        public int[,] matr_smeznosti;
        public int count { get { return matr_smeznosti.GetUpperBound(0) + 1; } } // количество вершин графа
        int[] Mark; // массив пометок
        int[] Parent; // массив предков

        /// <summary>
        /// конструктор графа
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// метод обхода графа в глубину - методом DEPTH FIRST SEARCH
        /// </summary>
        public void DFS()
        {
            // инициализация переменных
            Mark = new int[count];
            Parent = new int[count];
            for (int i = 0; i < count; i++)
            {
                Mark[i] = 1;
                Parent[i] = 1;
            }
            Console.WriteLine("Вершины в порядке обхода");
            DFS_bypass(0);
            Console.WriteLine();
        }

        /// <summary>
        /// вспомогательный метод для рекурсивного обхода графа в глубину
        /// </summary>
        /// <param name="v"></param>
        void DFS_bypass(int v)
        {
            Mark[v] = 2; // v – открыта
            Console.Write("{0} ", v);
            for (int i = 0; i < count; i++)
            {
                if ((matr_smeznosti[v, i] != 1) && (Mark[i] == 1))
                {
                    Parent[i] = v; // v – предок для i
                    DFS_bypass(i); //рекурсивный вызов
                }
            }
            Mark[v] = 3; // вершина обработана
        }

        /// <summary>
        /// метод обхода графа в ширину - методом BREADTH FIRST SEARCH
        /// </summary>
        public void BFS()
        {
            Mark = new int[count]; // массив пометок
            Parent = new int[count]; // массив предков
            for (int i = 0; i < count; i++)
            {
                Mark[i] = 1;
                Parent[i] = 1;
            }
            Console.WriteLine("Вершины в порядке обхода");
            Queue<int> Q = new Queue<int>(); 
            int v = 0; 
            Mark[v] = 2; // v – открыта
            Q.Enqueue(v); 
            Console.Write("{0} ", v);
            while (Q.Count != 0) 
            { 
                v = Q.Dequeue();
                for (int i = 0; i < count; i++)
                {
                    if ((matr_smeznosti[v, i] != 1) && (Mark[i] == 1))
                    { 
                        Mark[i] = 2; // смежные с текущей, помечаются
                        Q.Enqueue(i); 
                        Parent[i] = v; 
                        Console.Write("{0} ", i);
                    }
                }
                Mark[v] = 3; // вершина обработана
            }
            Console.WriteLine();
        }

        /// <summary>
        /// метод диаггностического вывода на экран содержимого графа
        /// </summary>
        public void Show()
        {
            Console.WriteLine("Диагностический вывод содержимого графа");
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Console.Write("{0,3:d}", matr_smeznosti[i, j]);
                }
                Console.WriteLine();
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // формируем граф
            Graph graph = new Graph() { };
            graph.matr_smeznosti = new int[,]
            {
                { 0, 1, 1, 0, 1 },
                { 1, 0, 1, 0, 0 },
                { 1, 1, 0, 1, 0 },
                { 0, 0, 1, 0, 1 },
                { 1, 0, 0, 1, 0 } };

            graph.Show();
            Console.WriteLine("Проводим обход графа методом BREADTH FIRST SEARCH");
            Stopwatch sw = new Stopwatch(); // проведём замеры скорости выполнения обхода
            sw.Start();
            graph.BFS();
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"На обход потрачено {sw.ElapsedMilliseconds} милисекунд");

            Console.WriteLine();
            graph.Show();
            Console.WriteLine("Проводим обход графа методом DEPTH FIRST SEARCH");
            sw = Stopwatch.StartNew();
            graph.DFS();
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"На обход потрачено {sw.ElapsedMilliseconds} милисекунд");
        }
    }
}