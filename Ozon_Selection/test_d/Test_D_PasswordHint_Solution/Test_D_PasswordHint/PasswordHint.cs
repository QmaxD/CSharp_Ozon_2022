using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Test_D_PasswordHint
{
    public static class PasswordHint
    {
        public static void StartPasswordHint(string inputFile, string outputFile)
        {
            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);
                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                int numberOfRequests = int.Parse(inputSR.ReadLine());
                //Console.WriteLine(numberOfRequests);

                do
                {
                    inputLine = inputSR.ReadLine();
                    Console.WriteLine(inputLine);

                    Random random = new();

                    bool smblInLowCase = false;
                    bool smblInUpperCase = false;
                    bool smblAsNumber = false;
                    bool smblAsVowel = false;
                    bool smblAsConsonant = false;
                    string result = "";

                    string lowCase = "abcdefghijklmnopqrstuvwxyz";// 26
                    string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";// 26
                    string number = "0123456789";// 10
                    string vowel = "aeiouyAEIOUY";// 12
                    string consonant = "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";// 40


                    if (Regex.Match(inputLine, @"(.*[a-z].*)+").Success)
                    {
                        smblInLowCase = true;
                        //Console.WriteLine("low " + smblInLowCase);
                    }
                    else
                    {
                        //Console.WriteLine("low " + smblInLowCase);
                        char c = lowCase[random.Next(0, 25)];
                        result += c;
                    }

                    if (Regex.Match(inputLine, @"(.*[A-Z].*)+").Success)
                    {
                        smblInUpperCase = true;
                        //Console.WriteLine("upper " + smblInUpperCase);
                    }
                    else
                    {
                        //Console.WriteLine("upper " + smblInUpperCase);
                        char c = upperCase[random.Next(0, 25)];
                        result += c;
                    }

                    if (Regex.Match(inputLine, @"(.*\d.*)+").Success)
                    {
                        smblAsNumber = true;
                        //Console.WriteLine("digit " + smblAsNumber);
                    }
                    else
                    {
                        //Console.WriteLine("digit " + smblAsNumber);
                        char c = number[random.Next(0, 9)];
                        result += c;
                    }

                    if (Regex.Match(inputLine, @"(.*[aeiouy]*)+", RegexOptions.IgnoreCase).Success)
                    {
                        smblAsVowel = true;
                        //Console.WriteLine("гласная " + smblAsVowel);
                    }
                    else
                    {
                        //Console.WriteLine("гласная " + smblAsVowel);
                        char c = vowel[random.Next(0, 11)];
                        result += c;
                    }

                    if (Regex.Match(inputLine, @"(.*[bcdfghjklmnpqrstvwxz]*)+", RegexOptions.IgnoreCase).Success)
                    {
                        smblAsConsonant = true;
                        //Console.WriteLine("согласная " + smblAsConsonant);
                    }
                    else
                    {
                        //Console.WriteLine("согласная " + smblAsConsonant);
                        char c = consonant[random.Next(0, 39)];
                        result += c;
                    }

                    //if (result != "")
                    //{
                    //    Console.WriteLine(result);
                    //    Console.WriteLine();
                    //}
                    result = inputLine + result;
                    Console.WriteLine(result);
                    Console.WriteLine();

                    numberOfRequests--;
                }
                while (numberOfRequests > 0);

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
