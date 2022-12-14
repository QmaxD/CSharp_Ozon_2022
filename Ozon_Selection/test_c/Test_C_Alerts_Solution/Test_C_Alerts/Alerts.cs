using System;
using System.Diagnostics;

namespace Test_C_Alerts
{
    public static class Alerts
    {
        public static void StartAlerts(string inputFile, string outputFile)
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
                int[] temp = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();
                int numberOfUsers = temp[0];
                int numberOfRequests = temp[1];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"users: {numberOfUsers} requests: {numberOfRequests}");

                int countMessage = 0;
                int[] collection = new int[numberOfUsers];
                int globalMessage = 0;

                do
                {
                    inputLine = inputSR.ReadLine();
                    int[] request = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();
                    int type = request[0];
                    int user = request[1] - 1;

                    Console.ForegroundColor = ConsoleColor.White;
                    if (type == 1)
                    {
                        countMessage++;
                        if (user == -1)
                            globalMessage = countMessage;
                        else
                            collection[user] = countMessage;
                    }
                    else
                    {
                        if (globalMessage > collection[user])
                        {
                            Console.WriteLine(globalMessage);
                            outSR.WriteLine(globalMessage);
                        }
                        else
                        {
                            Console.WriteLine(collection[user]);
                            outSR.WriteLine(collection[user]);
                        }
                    }
                    numberOfRequests--;
                }
                while (numberOfRequests > 0);

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

        }
    }
}
