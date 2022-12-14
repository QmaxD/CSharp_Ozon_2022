using System;
using System.Diagnostics;// LeadTime
using System.Linq;
using System.Text.RegularExpressions;// Regex, Validation

namespace Test_H_MapValidation
{
    public class Program
    {
        public static void Main(string[] args)// всё проходит менее чем 2 сек.
        {
            string numberTest = "25";
            string letterTest = "h";
            string inputFile = "E:\\Microsoft_Visual_Studio\\Project\\Ozon_selection\\pink_wolf_c43a_Sel_" + letterTest + "\\tests\\" + numberTest;
            string originFile = "E:\\Microsoft_Visual_Studio\\Project\\Ozon_selection\\pink_wolf_c43a_Sel_" + letterTest + "\\tests\\" + numberTest + ".a";
            string outputFile = "E:\\Microsoft_Visual_Studio\\Project\\Ozon_selection\\pink_wolf_c43a_Sel_" + letterTest + "\\tests\\" + numberTest + "out.a";

            MapValidation.StartMapValidation(inputFile, outputFile);

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
