using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace lab16
{
    class Program
    {
        private const int NUMBER = 100;
        private static CancellationTokenSource source = new CancellationTokenSource();
        private static CancellationToken token = source.Token;

        class Task7
        {
            private static int ProductCount = 0;
            private static BlockingCollection<string> products = new BlockingCollection<string>();

            private static void Put()
            {
                int NumOfProducts = 2;
                for (int i = 0; i < NumOfProducts; i++)
                {
                    products.Add("product" + ProductCount);
                    Console.WriteLine($"Product {ProductCount++} added");
                }
            }
            private static void Get()
            {
                while (!products.IsCompleted)
                {
                    Console.WriteLine($"Consomer takes {products.Take()}");
                }
            }

            public static void Start()
            {
                Task[] users = new Task[]
                {
                    Task.Factory.StartNew(Put),
                    Task.Factory.StartNew(Put),
                    Task.Factory.StartNew(Put),
                    Task.Factory.StartNew(Put),
                    Task.Factory.StartNew(Put)
                };
                Task[] consumers = new Task[]
                {
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get),
                    Task.Factory.StartNew(Get)
                };

                Task.WaitAll(consumers);
            }
        }
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            //Task1

            Task task1;
            for (int i = 0; i < 3; i++)
            {
                stopwatch.Start();
                task1 = Task.Factory.StartNew(FindSN);
                Console.WriteLine($"\nЗадача {i}: \nID - {task1.Id} \nStatus - {task1.Status}");
                task1.Wait();
                stopwatch.Stop();
                Console.WriteLine($"Время выполнения задачи в миллисекундах: {stopwatch.Elapsed.TotalMilliseconds}");
                stopwatch.Reset();
            }

            //Task2
            stopwatch.Start();
            Task task2 = Task.Factory.StartNew(FindSN);
            Console.WriteLine($"\nID - {task2.Id} \nStatus - {task2.Status}");
            Console.WriteLine("Введите q для выхода из процесса");
            if (Console.ReadKey().KeyChar == 'q')
            {
                source.Cancel();
            }
            task2.Wait();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения задачи в миллисекундах: {stopwatch.Elapsed.TotalMilliseconds}");

            //Task3-4
            Task3_4();

            //Task5
            stopwatch.Start();
            for (int i = 0; i < 5; i++)
            {
                int[] array = new int[1000000];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = j;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения цикла for: {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Reset();

            stopwatch.Start();
            Parallel.For(0, 5, (count) =>
            {
                int[] array = new int[1000000];
                Parallel.ForEach(array, (el) =>
                {
                    el++;
                });
            });
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения For/ForEach: {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Reset();

            //Task6
            void func()
            {
                for (int i = 0; i < 5; i++)
                {
                    int[] array = new int[1000000];
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = j;
                    }
                }
                Console.WriteLine("func done");
            }

            stopwatch.Start();
            func();
            func();
            func();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения 3-х func: {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Reset();

            stopwatch.Start();
            Parallel.Invoke(func, func, func);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения Invoke: {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Reset();

            //Task7
            Task7.Start();

            //Task8
            Task8();
        }
        private static void FindSN()
        {
            for (int i = 0; i < NUMBER; i++)
            {
                if (IsSN(i))
                {
                    Console.WriteLine($"{i} ");
                }
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Процесс прерван");
                    return;
                }
            }
        }
        private static bool IsSN(int number)
        {
            for (int i = 2; i < (number / 2); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Task3-4
        private const int x1 = 25;
        private const int x2 = 20;
        public static int func1()
        {
            return x1 * x2;
        }
        public static int func2()
        {
            return x1 + x2;
        }
        public static int func3()
        {
            return x1 - x2;
        }
        private static void Task3_4()
        {
            Task<int> task1 = new Task<int>(func1);
            Task<int> task2 = new Task<int>(func2);
            Task<int> task3 = new Task<int>(func3);
            task1.ContinueWith((t1) => Console.WriteLine($"Task1 Completed. Result: {t1.Result}"));
            task2.ContinueWith((t2) => Console.WriteLine($"Task2 Completed. Result: {t2.Result}"));
            task3.ContinueWith((t3) => Console.WriteLine($"Task3 Completed. Result: {t3.Result}"));
            task1.Start();
            task2.Start();
            task3.Start();
        }
        private static async void Task8()
        {
            void func()
            {
                int[] array = new int[1000000];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = j;
                }
            }

            Console.WriteLine("async started");
            await Task.Factory.StartNew(func);
            Console.WriteLine("async finished");
        }

    }

}
