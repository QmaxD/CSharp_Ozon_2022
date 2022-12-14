using System;
using System.Diagnostics;

namespace Test_H_MapValidation
{
    public static class MapValidation
    {
        public static void StartMapValidation(string inputFile, string outputFile) {

            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);
                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                inputLine = inputSR.ReadLine();
                int numberOfTests = int.Parse(inputLine);
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine($"{numberOfTests} test {str1}");

                do
                {
                    inputLine = inputSR.ReadLine();
                    int[] size = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();
                    int row = size[0];
                    int col = size[1];
                    //Console.ForegroundColor = ConsoleColor.Yellow;
                    //Console.Write($"\n{row} {col} ");
                    //Console.WriteLine($"map[{row}][{(col + 1) / 2 + (row - 1) / 2}]");
                    //Console.ForegroundColor = ConsoleColor.White;

                    char[][] map = new char[row][];
                    for (int i = 0; i < row; i++)
                    {
                        map[i] = new char[(col + 1) / 2 + (row - 1) / 2];
                    }

                    int numberRow = 0;
                    int numberCol = (row - 1) / 2;
                    int oddRow = 0;
                    do
                    {
                        inputLine = inputSR.ReadLine();
                        inputLine = inputLine.Trim('.');
                        char[] rowColor = inputLine.Split('.').Select(it => char.Parse(it)).ToArray();
                        int indexRowColor = 0;

                        for (int width = 0; width < map[numberRow].Length; width++)
                        {
                            if (width < numberCol || indexRowColor >= rowColor.Length)
                                map[numberRow][width] = '.';
                            else
                            {
                                map[numberRow][width] = rowColor[indexRowColor];
                                indexRowColor++;
                            }
                        }

                        oddRow++;
                        if (oddRow % 2 == 0)
                            numberCol--;
                        numberRow++;

                        row--;
                    } while (row > 0);

                    //PrintMap1();// печать полученного массива map[][]
                    //PrintMap2();// печать полученного массива map[][]

                    // поиск границ цвета
                    bool allTrue = true;
                    List<char> colors = new();
                    char color = '.';

                    int h = 0, w = 0;
                    while (h < map.Length && allTrue)
                    {
                        w = 0;
                        while (w < map[h].Length && allTrue)
                        {
                            if (map[h][w] != '.' && map[h][w] != 'x')
                            {
                                color = map[h][w];
                                map[h][w] = 'x';

                                if (colors.Contains(color))
                                {
                                    //Console.ForegroundColor = ConsoleColor.Red;
                                    //Console.WriteLine("NO\n");
                                    allTrue = false;
                                }
                                else
                                {
                                    //Console.ForegroundColor = ConsoleColor.Yellow;
                                    //Console.WriteLine("-- search color " + color + " --");
                                    SearchColorRegion(map, h, w, color);
                                    //Console.ForegroundColor = ConsoleColor.Green;
                                    //Console.WriteLine("YES\n");
                                    //PrintMap1();
                                    //PrintMap2();
                                }

                                colors.Add(color);// запишем цвет в массив
                            }

                            w++;
                        }

                        h++;
                    }

                    if (allTrue)
                    {
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine("ПОЛНОЕ СОВПАДЕНИЕ");
                        //Console.WriteLine("----------------------------------------------------");
                        outSR.WriteLine("YES");
                    }
                    else
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine("ERROR");
                        //Console.WriteLine("----------------------------------------------------");
                        outSR.WriteLine("NO");
                    }

                    numberOfTests--;
                }
                while (numberOfTests > 0);

                //Console.ForegroundColor = ConsoleColor.White;
                // ================================================== time ===============           
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Console.WriteLine("\ntime: " + ts);

                inputSR.Close();
                outSR.Close();
            }
            catch (Exception e) { Console.WriteLine("\nException: " + e.Message); }
            finally { Console.WriteLine("Executing finally block."); }

            void PrintMap_version_1(char[][] map)
            {
                for (int h = 0; h < map.Length; h++)
                {
                    //if (h % 2 != 0)
                    //{
                    //    Console.Write(" ");
                    //}
                    for (int w = 0; w < map[h].Length; w++)
                    {
                        //if (map[h][w] != '.')
                        //{
                        if (map[h][w] == 'x')
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(map[h][w] + " ");
                        //}
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }

            void PrintMap_version_2(char[][] map)
            {
                for (int h = 0; h < map.Length; h++)
                {
                    if (h % 2 != 0)
                    {
                        Console.Write(" ");
                    }
                    for (int w = 0; w < map[h].Length; w++)
                    {
                        if (map[h][w] != '.')
                        {
                            if (map[h][w] == 'x')
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            else
                                Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[h][w] + " ");
                        }
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }

            void SearchColorRegion(char[][] map, int row, int col, char color)
            {
                char verified = 'x';

                // ---  проверка в ряду справа  ---
                if (col + 1 < map[row].Length)
                {
                    if (map[row][col + 1] == color)
                    {
                        map[row][col + 1] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row, col + 1, color);
                    }
                }

                // ---  проверка внизу справа  ---
                if (row + 1 < map.Length)
                {
                    if (map[row + 1][col] == color)
                    {
                        map[row + 1][col] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row + 1, col, color);
                    }
                }

                // ---  проверка внизу слева  ---
                if (row + 1 < map.Length && col - 1 >= 0)
                {
                    if (map[row + 1][col - 1] == color)
                    {
                        map[row + 1][col - 1] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row + 1, col - 1, color);
                    }
                }

                // ---  проверка в ряду слева  ---
                if (col - 1 >= 0)
                {
                    if (map[row][col - 1] == color)
                    {
                        map[row][col - 1] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row, col - 1, color);
                    }
                }

                // ---  проверка вверху слева  ---
                if (row - 1 >= 0)
                {
                    if (map[row - 1][col] == color)
                    {
                        map[row - 1][col] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row - 1, col, color);
                    }
                }

                // ---  проверка вверху справа  ---
                if (row - 1 >= 0 && col + 1 < map[row - 1].Length)
                {
                    if (map[row - 1][col + 1] == color)
                    {
                        map[row - 1][col + 1] = verified;
                        //PrintMap2();
                        SearchColorRegion(map, row - 1, col + 1, color);
                    }
                }
            }

        }
    }
}
