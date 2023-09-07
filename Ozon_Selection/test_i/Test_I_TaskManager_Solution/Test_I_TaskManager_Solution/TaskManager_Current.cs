//using System;

//namespace Test_I_TaskManager
//{
//    public class TaskManager_Current
//    {
//        static int cpuAmount = 0;// количество процессоров
//        static int taskAmount = 0;// количество задач
//        static int[]? cpuPowerSortArray;// энергопотребление процессоров
//        static SortedSet<int> cpuPowerList = new SortedSet<int>();
//        static SortedDictionary<long, List<int>> cpuInUseMap = new SortedDictionary<long, List<int>>();
//        static Queue<int> timeStartQueue = new Queue<int>();// очередь времени поступления задач
//        static Queue<long> timeDurationQueue = new Queue<long>();// очередь времени использования процессоров
//        static long powerResult = 0L;// потребляемая энергия

//        public static void Main(string[] args)
//        {
//            var info = Console.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
//            cpuAmount = info[0];
//            taskAmount = info[1];

//            cpuPowerSortArray = Console.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
//            foreach (int power in cpuPowerSortArray)// заполним SortedSet<int> cpuPowerList;
//                cpuPowerList.Add(power);

//            for (int task = 1; task <= taskAmount; task++) {// заполняем очереди поступления задач
//                var tasks = Console.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
//                timeStartQueue.Enqueue(tasks[0]);
//                timeDurationQueue.Enqueue(tasks[1]);
//            }
                
//            // **** WORK & GET POWER ****
//            for (int task = 1; task <= taskAmount; task++)
//            {
//                long timeStart = timeStartQueue.Dequeue();
//                long timeDuration = timeDurationQueue.Dequeue();
//                if (cpuInUseMap.Count() > 0)
//                {
//                    СheckFreeCPU(timeStart);
//                }
//                if (cpuPowerList.Count() > 0)
//                {
//                    UseCPU(timeStart, timeDuration);
//                }
//            }
//            Console.WriteLine(powerResult);
//        }
        
//        private static void СheckFreeCPU(long tickStart)
//        {
//            long duration = 0L;
//            while (cpuInUseMap != null && cpuInUseMap.Count() > 0)
//            {
//                duration = cpuInUseMap.First().Key;
//                var powerList = cpuInUseMap.First().Value;
//                if (duration <= tickStart)
//                {
//                    foreach (var power in powerList)
//                    {
//                        cpuPowerList.Add(power);
//                    }
//                    cpuInUseMap.Remove(duration);
//                }
//                else
//                {
//                    break;
//                }
//            }
//        }

//        private static void UseCPU(long timeStart, long timeDuration)
//        {
//            int cpuPower = cpuPowerList.First();// берем процессор с наименьшей мощностью
//            cpuPowerList.Remove(cpuPower);// и удаляем из списка
//            powerResult += (long)cpuPower * timeDuration;// высчитываем общую мощность
//            if (cpuInUseMap.ContainsKey(timeDuration + timeStart))
//            {
//                List<int>? powerList;
//                cpuInUseMap.TryGetValue(timeDuration + timeStart, out powerList);
//                powerList.Add(cpuPower);
//                cpuInUseMap.Remove(timeDuration + timeStart);
//                cpuInUseMap.Add(timeDuration + timeStart, powerList);// записываем в Map k=продолжительность v=мощность
//            }
//            else
//            {
//                cpuInUseMap.Add(timeDuration + timeStart, new List<int>() { cpuPower });// записываем в Map k=продолжительность v=мощность
//            }
//        }
        
//    }
//}
