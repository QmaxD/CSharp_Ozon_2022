namespace Test_E_Report
{
    public class Program
    {
        public static void Main(string[] args)// всё проходит менее чем 2 сек.
        {

            // test 25 time: 00:00:15.8188201 //Report_3 - ошибка
            // test 30 time: 00:00:13.1758996 //00:00:10.8064469 //00:00:00.1480890
            // test 40 time: 00:00:27.6896036 //00:00:00.1881056
            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string numberTest = "36";
            string letterTest = "e";
            string inputFile = path + letterTest + @"\Tests\" + numberTest;
            string originFile = path + letterTest + @"\Tests\" + numberTest + ".a";
            string outputFile = path + letterTest + @"\Tests\" + numberTest + "out.a";

            Report.StartReport(inputFile, outputFile);
            //Report_2.StartReport(inputFile, outputFile);
            //Report_3.StartReport(inputFile, outputFile);

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
