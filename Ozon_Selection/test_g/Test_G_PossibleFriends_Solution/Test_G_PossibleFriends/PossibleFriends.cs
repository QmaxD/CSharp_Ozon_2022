using System;
using System.Diagnostics;

namespace Test_G_PossibleFriends
{
    public static class PossibleFriends
    {
        public static void StartPossibleFriends(string inputFile, string outputFile)
        {
            
            string? inputLine;
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSR = new(outputFile);

                // ================================================== time ===============
                Stopwatch stopwatch = new();
                stopwatch.Start();

                inputLine = inputSR.ReadLine();
                int[] temp = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();

                int numberOfUsers = temp[0];
                int numberOfInputFriendPairs = temp[1];

                List<User> users = new(numberOfUsers);
                int count = 1;
                while (count <= numberOfUsers)
                {
                    users.Add(new User(count));
                    count++;
                }

                while (numberOfInputFriendPairs > 0)
                {
                    inputLine = inputSR.ReadLine();
                    int[] inputFriendPairs = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{inputFriendPairs[0]} {inputFriendPairs[1]}: ");
                    Console.ForegroundColor = ConsoleColor.White;

                    users[inputFriendPairs[0] - 1].AddFriend(inputFriendPairs[1]);
                    users[inputFriendPairs[1] - 1].AddFriend(inputFriendPairs[0]);

                    numberOfInputFriendPairs--;
                }

                //Console.WriteLine();
                //PrintAllFriends(users);
                //Console.WriteLine();

                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("search future friends");
                //Console.ForegroundColor = ConsoleColor.White;

                SearchFutureFriends(users);
                PrintFutureFriends(users, ref outSR);

                // ================================================== time ===============           
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Console.WriteLine("\ntime: " + ts);

                inputSR.Close();
                outSR.Close();
            }
            catch (Exception e) { Console.WriteLine("\nException: " + e.Message); }
            finally { Console.WriteLine("Executing finally block."); }

            // метод: вывод полученных данных
            void PrintAllFriends(List<User> users)
            {
                foreach (var user in users)
                {
                    if (user.CountFriends == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{user.ID} have 0 friends");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{user.ID} have {user.CountFriends} friends: ");
                        foreach (var friend in user.Friends)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(friend);
                            Console.Write(" ");
                        }
                        Console.Write("\b");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            // метод: алгоритм поиска совпадений друзей
            void SearchFutureFriends(List<User> users)
            {
                foreach (User sourceUser in users)// у кого ищем, источник
                {
                    int maxMatches = 0;// счетчик максимальных совпадений

                    if (sourceUser.CountFriends != 0)// если у источника есть друзья
                    {
                        foreach (User searchUser in users)// перечисляем юзеров, ищем юзера
                        {
                            if (sourceUser != searchUser)// если номер источника не равен искомому
                            {
                                if (!(sourceUser.Friends).Contains(searchUser.ID))// если искомый вне списка друзей источника
                                {
                                    int numberOfMatches = 0;// счетчик совпадений

                                    foreach (int sourceFriend in sourceUser.Friends)// перечисляем друзей источника
                                    {
                                        if ((searchUser.Friends).Contains(sourceFriend))// если список друзей искомого содержит друга источника
                                            numberOfMatches++;// счетчик совпадений +1
                                    }

                                    if (maxMatches < numberOfMatches)
                                    {
                                        (sourceUser.FutureFriends).Clear();
                                        (sourceUser.FutureFriends).Add(searchUser.ID);
                                        maxMatches = numberOfMatches;
                                    }
                                    else if (maxMatches == numberOfMatches && maxMatches != 0)
                                    {
                                        (sourceUser.FutureFriends).Add(searchUser.ID);
                                    }

                                }
                            }
                        }
                    }
                }
            }

            // метод: вывод на печать совпадения
            void PrintFutureFriends(List<User> users, ref StreamWriter outSR)
            {
                foreach (User sourceUser in users)
                {
                    if ((sourceUser.FutureFriends).Count == 0)
                    {
                        Console.WriteLine("0");
                        outSR.WriteLine("0");
                    }
                    else
                    {
                        int maxIndexInList = 0;
                        foreach (int index in (sourceUser.FutureFriends))
                        {
                            Console.Write(index);
                            outSR.Write(index);

                            maxIndexInList++;
                            if (maxIndexInList < (sourceUser.FutureFriends).Count)
                            {
                                Console.Write(" ");
                                outSR.Write(" ");
                            }
                            else
                            {
                                Console.WriteLine();
                                outSR.WriteLine();
                            }
                        }
                    }
                }
            }

        }
    }
}
