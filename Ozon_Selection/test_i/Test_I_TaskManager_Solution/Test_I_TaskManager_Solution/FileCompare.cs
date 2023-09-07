using System;

namespace Test_I_TaskManager
{
    public static class FileCompare
    {
        private static List<String> testsResult = new List<string>();

        public static void AddToResult(String str)
        {
            testsResult.Add(str);
        }

        public static void AddToElement(String str)
        {
            int n = testsResult.Count - 1;
            testsResult[n] += " " + str;
        }

        public static void PrintResult()
        {
            foreach (String str in testsResult)
                Console.WriteLine(str);
        }


        public static void StartFilesCompare(string file1, string file2, string testNumber)
        {
            bool notCompare = false;
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            if (file1 != file2)
                notCompare = true;

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);
            if (fs1.Length != fs2.Length && !notCompare)
            {
                fs1.Close();
                fs2.Close();
                notCompare = true;
            }

            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
                if (file1byte != file2byte)
                    notCompare = true;
            }
            while (file1byte != -1 && !notCompare);

            fs1.Close();
            fs2.Close();

            if ((file1byte - file2byte) == 0)
                notCompare = true;

            if (!notCompare)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(testNumber + " ERROR: Files are different!");
                testsResult.Add(testNumber + " ERROR: Files are different!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(testNumber + " Files are identical. Congratulations!");
                testsResult.Add(testNumber + " Files are identical. Congratulations!");
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

    }
}
