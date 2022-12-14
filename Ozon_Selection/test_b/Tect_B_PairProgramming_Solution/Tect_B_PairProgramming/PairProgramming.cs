using System;
using System.Diagnostics;

namespace Test_B_PairProgramming
{
    public static class PairProgramming
    {
        public static void StartPairProgramming(string inputFile, string outputFile)
        {

            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);
                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                inputLine = inputSR.ReadLine();
                int numberOfTest = int.Parse(inputLine);

                do
                {
                    inputLine = inputSR.ReadLine();
                    int numberOfDevelopers = int.Parse(inputLine);
                    List<int> devs = new(numberOfDevelopers);

                    inputLine = inputSR.ReadLine();
                    devs = inputLine.Split(' ').Select(it => int.Parse(it)).ToList();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var item in devs)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    for (int i = 1; i <= numberOfDevelopers / 2; i++)
                    {
                        int minValue = int.MaxValue;
                        int sourceIndex = 0;
                        int searchIndex = 0;

                        sourceIndex = GetFirstDev(devs);

                        Console.Write("{");
                        for (int k = 0; k < devs.Count; k++)
                        {
                            if (devs[k] != -1 && k != sourceIndex)
                            {

                                Console.Write(k + "(");
                                int temp = devs[sourceIndex] - devs[k];

                                if (temp < 0)
                                    temp = temp * (-1);
                                Console.Write(temp + ") ");
                                if (temp < minValue)
                                {
                                    minValue = temp;
                                    searchIndex = k;
                                }
                            }
                        }
                        devs[sourceIndex] = -1;
                        devs[searchIndex] = -1;

                        Console.Write("\b}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($": {sourceIndex + 1} {searchIndex + 1} - {minValue}");
                        outSR.WriteLine($"{sourceIndex + 1} {searchIndex + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (numberOfTest > 1)
                        outSR.WriteLine();
                    Console.WriteLine();

                    numberOfTest--;
                }
                while (numberOfTest > 0);

                // ================================================== time ===============           
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Console.WriteLine("time: " + ts);

                inputSR.Close();
                outSR.Close();
            }
            catch (Exception e) { Console.WriteLine("Exception: " + e.Message); }
            finally { Console.WriteLine("Executing finally block."); }

            int GetFirstDev(List<int> devs)
            {
                for (int h = 0; h < devs.Count; h++)
                {
                    if (devs[h] != -1)
                    {
                        Console.WriteLine("search: " + h);
                        return h;
                    }
                }
                return -1;
            }

        }
    }
}
