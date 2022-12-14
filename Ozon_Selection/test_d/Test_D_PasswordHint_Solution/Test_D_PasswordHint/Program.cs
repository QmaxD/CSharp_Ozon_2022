namespace Test_D_PasswordHint
{
    public class Program
    {
        public static void Main(string[] args)// всё проходит менее чем 2 сек.
        {
            // НЕ ВОЗМОЖНО проверить, т.к. выходные файлы будут всегда разными
            string path = @"E:\GITHUB\Project_CSharp_Ozon\Ozon_2022\Ozon_Selection\test_";
            string numberTest = "25";
            string letterTest = "d";
            string inputFile = path + letterTest + @"\Tests\" + numberTest;
            string originFile = path + letterTest + @"\Tests\" + numberTest + ".a";
            string outputFile = path + letterTest + @"\Tests\" + numberTest + "out.a";

            PasswordHint.StartPasswordHint(inputFile, outputFile);

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
