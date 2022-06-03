using System;
using System.IO;
using System.Linq;

namespace Задание01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Укажите путь к файлу .txt: ");
            string path1 = Console.ReadLine();

            try
            {
                ReadSomeFile(path1);
            }
            catch
            {
                Console.WriteLine("Неверно указан путь");
                Console.WriteLine("Пример: C:\\Folder\\example.txt");
            }
        }

        private static void ReadSomeFile(string path1)
        {
            string[] lines = File.ReadAllLines(path1).ToArray();

            int rows = lines.Length - 1;
            int columns = 2;

            Console.WriteLine("Пожалуйста, подождите. Идёт обработка...");

            // запись каждой строки в массив массивов
            string[][] res = new string[lines.Length][];
            int n = 0;
            foreach (var line in lines)
            {
                res[n] = line.Split(' ');
                n++;
            }

            int[,] treesCords = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    treesCords[i, j] = Convert.ToInt32(res[i + 1][j]);
                }
            }

            SortTreesArray(treesCords);
            FindAndPrintEmptySpace(treesCords);
        }

        private static void SortTreesArray(int[,] treesCords)
        {
            // сортировка (по возрастанию) по номеру ряда, сохраняя с ним и место дерева
            int t0, t1;
            for (int i = 0; i < treesCords.GetLength(0); i++)
            {
                for (int j = 0; j < treesCords.GetLength(0) - 1; j++)
                {
                    if (treesCords[j, 0] > treesCords[j + 1, 0])
                    {
                        t0 = treesCords[j + 1, 0];
                        t1 = treesCords[j + 1, 1];

                        treesCords[j + 1, 0] = treesCords[j, 0];
                        treesCords[j + 1, 1] = treesCords[j, 1];

                        treesCords[j, 0] = t0;
                        treesCords[j, 1] = t1;
                    }
                }
            }

            // сортировка (по возрастанию) места дерева, если номер ряда одинаковый
            for (int i = 0; i < treesCords.GetLength(0); i++)
            {
                for (int j = 0; j < treesCords.GetLength(0) - 1; j++)
                {
                    if (treesCords[j, 0] == treesCords[j + 1, 0])
                    {
                        if (treesCords[j, 1] > treesCords[j + 1, 1])
                        {
                            t1 = treesCords[j + 1, 1];

                            treesCords[j + 1, 1] = treesCords[j, 1];

                            treesCords[j, 1] = t1;
                        }

                    }
                }
            }
        }

        private static void FindAndPrintEmptySpace(int[,] treesCords)
        {
            for (int i = treesCords.GetLength(0) - 1; i >= 1; i--)
            {
                for (int j = treesCords.GetLength(0) - 1; j >= 1; j--)
                {
                    if (treesCords[j, 0] == treesCords[j - 1, 0])
                    {
                        if (treesCords[j, 1] - treesCords[j - 1, 1] == 14)
                        {
                            Console.WriteLine("Найдены 13 свободных мест для посадки деревьев.");
                            Console.WriteLine($"Ряд: {treesCords[j, 0]} Место: {treesCords[j - 1, 1] + 1}");

                            return;
                        }

                    }
                }
            }
        }
    }
}
