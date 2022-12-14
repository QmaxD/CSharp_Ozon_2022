using System.Diagnostics;

namespace Test_E_Report
{
    public static class Report_3
    {
        public static void StartReport(string inputFile, string outputFile)
        {

            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);
                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                int numberOfTest = int.Parse(inputSR.ReadLine());
                Console.WriteLine(numberOfTest + "\n");
                do
                {
                    int numberOfDays = int.Parse(inputSR.ReadLine());
                    Console.WriteLine(numberOfDays);

                    inputLine = inputSR.ReadLine();
                    //Console.WriteLine(inputLine);

                    List<int> report = inputLine.Split(' ').Select(it => int.Parse(it)).ToList();
                    bool seriatim = true;

                    //foreach (var item in report)
                    //    Console.Write(item + " ");
                    //Console.WriteLine();

                    do
                    {
                        int value = report[0];
                        int indexLast = report.LastIndexOf(value);
                        int[] result = new int[indexLast + 1];
                        report.CopyTo(0, result, 0, indexLast + 1);

                        if (value != Array.Find(result, it => it != value))
                            seriatim = false;

                    }
                    while (report.Count > 0 && seriatim);

                    if (seriatim)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("YES");
                        Console.ForegroundColor = ConsoleColor.White;
                        outSR.WriteLine("YES");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("NO");
                        Console.ForegroundColor = ConsoleColor.White;
                        outSR.WriteLine("NO");
                    }
                    numberOfTest--;
                }
                while (numberOfTest > 0);

                // ================================================== time ===============           
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Console.WriteLine("\ntime: " + ts);

                inputSR.Close();
                outSR.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }
    }
}
