namespace Test_C_Alerts
{
    public class Program
    {
        public static void Main(string[] args)// всё проходит менее чем 2 сек.
        {
            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string numberTest = "30";
            string letterTest = "c";
            string inputFile = path + letterTest + @"\Tests\" + numberTest;
            string originFile = path + letterTest + @"\Tests\" + numberTest + ".a";
            string outputFile = path + letterTest + @"\Tests\" + numberTest + "out.a";

            Alerts.StartAlerts(inputFile, outputFile);

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
