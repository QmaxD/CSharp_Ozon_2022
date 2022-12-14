namespace Test_G_PossibleFriends
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // test 12 time: 00:00:07.9759228 - может пройти
            // test 13
            // test 14
            // test 15 time: 00:02:13.2877915 time: 00:02:00.3389587
            // test 1
            // test 1
            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string numberTest = "15";
            string letterTest = "g";
            string inputFile = path + letterTest + @"\Tests\" + numberTest;
            string originFile = path + letterTest + @"\Tests\" + numberTest + ".a";
            string outputFile = path + letterTest + @"\Tests\" + numberTest + "out.a";

            PossibleFriends.StartPossibleFriends(inputFile, outputFile);
            
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
