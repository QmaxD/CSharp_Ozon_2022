namespace Test_F_TimeSlices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // test 10 time: 00:00:00.4591606
            // test 11 time: 00:01:05.2314268
            // test 12 time: 00:01:02.9313259
            // test 13 time: 00:00:44.8423921
            // test 14 time: 00:00:47.2287847
            // test 15 time: 00:00:54.7279643
            // ...
            // test 25 time: 00:01:16.4906152
            // ...
            // test 35 time: 00:02:00.2212861
            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string numberTest = "25";
            string letterTest = "f";
            string inputFile = path + letterTest + @"\Tests\" + numberTest;
            string originFile = path + letterTest + @"\Tests\" + numberTest + ".a";
            string outputFile = path + letterTest + @"\Tests\" + numberTest + "out.a";

            TimeSlices.StartTimeSlices(inputFile, outputFile);

            bool resultCompareFiles = FileCompare.StartFilesCompare(originFile, outputFile);
            if (resultCompareFiles)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCOMPARE FILES: " + (resultCompareFiles.ToString()).ToUpper() + " !");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCOMPARE FILES: " + (resultCompareFiles.ToString()).ToUpper() + " !");
            }
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
