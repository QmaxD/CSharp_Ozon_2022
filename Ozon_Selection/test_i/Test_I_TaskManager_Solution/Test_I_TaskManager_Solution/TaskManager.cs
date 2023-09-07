using System;
using System.Diagnostics;

namespace Test_I_TaskManager
{
    public class TaskManager
    {
        int cpuAmount = 0;// количество процессоров

        int taskAmount = 0;// количество поступаемых задач

        int[]? cpuPowerSortArray;// энегопотребление процессоров

        SortedSet<int> cpuPowerList = new SortedSet<int>();

        SortedDictionary<long, List<int>> cpuInUseMap = new SortedDictionary<long, List<int>>();

        Queue<int> timeStartQueue = new Queue<int>();// очередь времени поступления задач

        Queue<long> timeDurationQueue = new Queue<long>();// очередь времени использования процессоров

        long powerResult = 0L;// потребляемая энергия

        public void StartTaskManager(string inputFile, string outputFile)
        {
            try
            {
                StreamReader inputSR = new(inputFile);
                StreamWriter outSW = new(outputFile);

                var info = inputSR.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                cpuAmount = info[0];
                taskAmount = info[1];

                cpuPowerSortArray = inputSR.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                foreach (int power in cpuPowerSortArray)// заполним SortedSet<int> cpuPowerList;
                    cpuPowerList.Add(power);

                for (int task = 1; task <= taskAmount; task++)// заполняем очереди поступления задач
                {
                    var tasks = inputSR.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                    timeStartQueue.Enqueue(tasks[0]);
                    timeDurationQueue.Enqueue(tasks[1]);
                }
                
                // **** WORK & GET POWER ****
                for (int task = 1; task <= taskAmount; task++)
                {
                    //PrintFreeCPU();
                    //PrintWorkingCPU();

                    long timeStart = timeStartQueue.Dequeue();
                    long timeDuration = timeDurationQueue.Dequeue();
                    //Console.WriteLine(timeStart + " : " + timeDuration);

                    if (cpuInUseMap.Count() > 0)
                        СheckFreeCPU(timeStart);

                    if (cpuPowerList.Count() > 0)
                        UseCPU(timeStart, timeDuration);

                    //PrintFreeCPU();
                    //PrintWorkingCPU();
                    //Console.WriteLine("------------------");
                }
                Console.WriteLine("powerResult: " + powerResult);
                outSW.WriteLine(powerResult);
         
                inputSR.Close();
                outSW.Close();
            }
            catch (Exception e) { Console.WriteLine("\nException: " + e.Message); }
            //finally { Console.WriteLine("Executing finally block."); }
        }
        
        private void СheckFreeCPU(long tickStart)
        {
            long duration = 0L;
            while (cpuInUseMap != null && cpuInUseMap.Count() > 0)
            {
                duration = cpuInUseMap.First().Key;
                var powerList = cpuInUseMap.First().Value;

                if (duration <= tickStart)
                {
                    foreach (var power in powerList)
                    {
                        cpuPowerList.Add(power);
                        //Console.WriteLine("СheckFreeCPU: k=" + duration + " v=" + power);
                    }
                    cpuInUseMap.Remove(duration);
                }
                else
                    break;
            }
        }

        private void UseCPU(long timeStart, long timeDuration)
        {
            int cpuPower = cpuPowerList.First();// берем процессор с наименьшей мощностью
            cpuPowerList.Remove(cpuPower);// и удаляем из списка
            powerResult += (long)cpuPower * timeDuration;// высчитываем общую мощность
            if (cpuInUseMap.ContainsKey(timeDuration + timeStart))
            {
                List<int>? powerList;
                cpuInUseMap.TryGetValue(timeDuration + timeStart, out powerList);
                powerList.Add(cpuPower);
                cpuInUseMap.Remove(timeDuration + timeStart);
                cpuInUseMap.Add(timeDuration + timeStart, powerList);// записываем в Map k=продолжительность v=мощность
            }
            else
                cpuInUseMap.Add(timeDuration + timeStart, new List<int>() { cpuPower });// записываем в Map k=продолжительность v=мощность
            //Console.WriteLine("UseCPU: v=" + cpuPower);
        }

        private void PrintFreeCPU()
        {
            Console.Write("PrintFreeCPU:");
            foreach (var elem in cpuPowerList)
                Console.Write(" [v=" + elem + "];");
            Console.WriteLine();
        }

        private void PrintWorkingCPU()
        {
            Console.Write("PrintWorkingCPU:");
            foreach (var item in cpuInUseMap)
                Console.Write(" [k=" + item.Key + " v=" + item.Value + "];");
            Console.WriteLine();
        }
        
    }
}
