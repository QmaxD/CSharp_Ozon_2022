using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Test_F_TimeSlices
{
    public static class TimeSlices
    {
        public static void StartTimeSlices(string inputFile, string outputFile)
        {

            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);
                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                int numberOfTest = int.Parse(inputSR.ReadLine());// количество тестов
                Console.WriteLine(numberOfTest);
                bool timeCrossingError = false;// проверка на пересечение отрезков времени
                int countTest = 1;

                do
                {
                    bool getNumber = false;
                    int numberOfTime = 0;
                    while (getNumber == false)
                        getNumber = int.TryParse(inputSR.ReadLine(), out numberOfTime);// количество отрезков времени

                    Console.WriteLine("\nколичество строк: " + numberOfTime + " номер теста: " + countTest);
                    bool timePeriodError = false;// проверка на валидацию или реальность периода времени

                    List<TimeOnly[]> timeList = new();// лист хранит период времени (начало и конец отрезка)
                    timeCrossingError = false;// в начале сбросим проверку на пересечение отрезков времени

                    do
                    {
                        inputLine = inputSR.ReadLine();// получаем строку периода времени
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(inputLine);
                        Console.ForegroundColor = ConsoleColor.White;

                        string[] timePeriod = inputLine.Split('-').Select(it => it).ToArray();// сплитим в массив строк (всего будет 2)
                        TimeOnly[] times = new TimeOnly[timePeriod.Length];// создаем массив времени для хранения начала и конца отрезка
                        timePeriodError = false;// в начале сбросим проверку на валидацию и реальность отрезка времени

                        int index = 0;
                        string temp = "";// для записи доп. инфы к отрезку времени (почему отрезок с ошибкой)
                        while (index < timePeriod.Length && !timePeriodError)
                        {
                            if (
                                (Regex.Match(timePeriod[index], @"[2][0-3]:[0-5]\d:[0-5]\d", RegexOptions.Compiled).Success) ||
                                (Regex.Match(timePeriod[index], @"[0-1]\d:[0-5]\d:[0-5]\d", RegexOptions.Compiled).Success)
                               )// проверка на валидацию, время соответствует 00:00:00 - 23:59:59
                                times[index] = TimeOnly.ParseExact(timePeriod[index], "HH:mm:ss");// парсим в готовый массив TimeOnly
                            else// если не прошел валидацию
                            {
                                temp = " error Regex";
                                timePeriodError = true;// цикл по принятию строк периодов времени прекратится по условию
                                timeCrossingError = true;// цикл по принятию строк периодов времени прекратится по условию
                                break;// прекращаем этот цикл while по валидации (экономим один запрос)
                            }
                            index++;
                        }

                        if (times[0] > times[1])// если отрезок времени реален (начало раньше чем конец, равны быть могут)
                        {
                            temp = " error Period";
                            timePeriodError = true;// цикл по принятию строк периодов времени прекратится по условию
                            timeCrossingError = true;// цикл по принятию строк периодов времени прекратится по условию
                            //break;
                        }
                        // выведем отрезок, который получился (для сверки с входящей строкой)
                        Console.WriteLine($"{times[0].ToString("HH:mm:ss")}-{times[1].ToString("HH:mm:ss")}" + temp);

                        index = 0;// сбросим индекс
                        while (index < timeList.Count && !timePeriodError && !timeCrossingError)// по булеву может быть пропущен (экономия)
                                                                                                // сверяем пересекаются ли периоды отрезков времени введенные ранее
                        {
                            if (
                                (times[0].Ticks >= timeList[index][0].Ticks && times[1].Ticks <= timeList[index][1].Ticks) ||
                                (times[0].Ticks >= timeList[index][0].Ticks && times[0].Ticks <= timeList[index][1].Ticks) ||
                                (times[1].Ticks >= timeList[index][0].Ticks && times[1].Ticks <= timeList[index][1].Ticks) ||
                                (times[0].Ticks <= timeList[index][0].Ticks && times[1].Ticks >= timeList[index][1].Ticks)
                               )
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("последний период пересекается с предыдущими");
                                timeCrossingError = true;// цикл по принятию строк периодов времени прекратится по условию
                                break;// прекращаем этот цикл while по сверке (экономим от одного запроса минимум)
                            }
                            index++;
                        }
                        timeList.Add(times);// заносим проверенный период в лист TimeOnly, чтобы потом его сверять

                        numberOfTime--;
                    }
                    while (numberOfTime > 0 && !timePeriodError && !timeCrossingError);// по булеву может быть завершен досрочно (экономия)

                    //while (numberOfTime > 1)// если цикл по принятию периодов времени завершен досрочно 
                    //{
                    //    inputLine = inputSR.ReadLine();// будем в холостую принимать строки периодов (т.к. некуда деваться)
                    //    numberOfTime--;// сбрасываем счетчик входящих строк
                    //}

                    //Console.ForegroundColor = ConsoleColor.Green;
                    //foreach (var elem in timeList)// посмотрим что в лист TimeOnly (выведем сокращенные тики)
                    //    Console.WriteLine((elem[0].Ticks) + " " + (elem[1].Ticks));

                    Console.ForegroundColor = ConsoleColor.Red;
                    if (timeCrossingError || timePeriodError)// если хоть один флаг сработал ранее
                    {
                        Console.WriteLine("NO");
                        outSR.WriteLine("NO");
                    }
                    else
                    {
                        Console.WriteLine("YES");
                        outSR.WriteLine("YES");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    countTest++;
                    numberOfTest--;
                }
                while (numberOfTest > 0);

                // ================================================== time ===============           
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Console.WriteLine("\ntime: " + ts);

                inputSR.Close();
                outSR.Close();
            }
            catch (Exception e) { Console.WriteLine("Exception: " + e.Message); }
            finally { Console.WriteLine("Executing finally block."); }

        }
    }
}
