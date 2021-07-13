using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace HashSetString
{
    public class Program
    {
        public class TheBenchmark
        {
            /// <summary>
            /// класс для работы
            /// </summary>
            public class HashString
            {
                public string Str { get; set; }
                public override bool Equals(object obj)
                {
                    var HS = obj as HashString;

                    if (HS == null)
                        return false;

                    return string.Compare(Str, HS.Str) == 0 ? true : false;
                }

                public override int GetHashCode()
                {
                    int strHashCode = string.Compare(Str, string.Empty) == 0 ? Str.GetHashCode() : 0;
                    return strHashCode;
                }
            }

            /// <summary>
            /// генерируем случайные слова
            /// </summary>
            public static string GetRandomString()
            {
                int num_letters = 20; // количество символов в строке
                char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); // массив букв, которые будем использовать

                Random rand = new Random(); // cоздаем генератор случайных чисел


                string word = string.Empty; // переменная для хранения строки
                for (int j = 1; j <= num_letters; j++)
                {
                    // случайное числ от 0 до 25
                    // для выбора буквы из массива букв.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    word += letters[letter_num]; // добавляем символ в строку
                }

                // Добавьте слово в список.
                return word;
            }

            public static string[] strArr = new string[10000];
            public static HashSet<HashString> HS = new HashSet<HashString>();
            public static string sF = string.Empty; // переменная для хранения строки для поиска

            public TheBenchmark()
            {
                int l = strArr.Length; // количество циклов для формирования наборов данных
                string s = string.Empty; // временная переменная для хранения сгенерированной случайной строки
                for (int j = 0; j < l; j++)
                {
                    s = GetRandomString();
                    strArr[j] = s;
                    var hs = new HashString() { Str = s };
                    HS.Add(hs);
                    if (j == 4591)
                        sF = s;
                }
            }

            [Benchmark(Description = "SearchHS")]
            public bool searchHS()
            {
                var searchHS = new HashString() { Str = sF };
                return HS.Contains(searchHS);
            }

            [Benchmark(Description = "SearchSTR")]
            public int searchStr()
            {
                var searchStr = Array.IndexOf(strArr, sF);
                return searchStr;
            }
        }

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TheBenchmark>();
        }
    }
}
