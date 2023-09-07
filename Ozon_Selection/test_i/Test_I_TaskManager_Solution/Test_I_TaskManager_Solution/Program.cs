using System.Diagnostics;

namespace Test_I_TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string testNumber = "01";
            string testLetter = "i";
            int maxTestNumber = 20;
            FileCompare.AddToResult("Задача " + testLetter);

            for (int task = 1; task <= maxTestNumber; task++)
            {
                Stopwatch stopwatchForTask = new();
                stopwatchForTask.Start();

                testNumber = String.Format("{0:d2}", task);
                string inputFile = path + testLetter + @"\Tests\" + testNumber;
                string originFile = path + testLetter + @"\Tests\" + testNumber + ".a";
                string outputFile = path + testLetter + @"\Tests\" + testNumber + "out.a";

                TaskManager taskManager = new TaskManager();
                taskManager.StartTaskManager(inputFile, outputFile);

                stopwatchForTask.Stop();
                FileCompare.StartFilesCompare(originFile, outputFile, testNumber);

                TimeSpan tsForTask = stopwatchForTask.Elapsed;
                Console.WriteLine("Execution time: " + tsForTask);
                FileCompare.AddToElement(" - " + tsForTask);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
            }

            FileCompare.PrintResult();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Full execution time: " + ts);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
