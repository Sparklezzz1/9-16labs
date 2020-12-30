using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace lab15
{
    class Program
    {
        static Mutex mutexObj = new Mutex();
        static bool turn = false;
        static void Main(string[] args)
        {
            //1
            Process notepad = Process.Start("notepad.exe");
            Thread.Sleep(1000);
            notepad.Kill();

            StreamWriter writer = new StreamWriter(@"D:/1.txt");

            foreach (Process Allprocesses in Process.GetProcesses())
            {
                Console.WriteLine($"ID: {Allprocesses.Id} \nProcessName: {Allprocesses.ProcessName} \nBasePriority: {Allprocesses.BasePriority}");
                writer.WriteLine($"ID: {Allprocesses.Id} \nProcessName: {Allprocesses.ProcessName} \nBasePriority: {Allprocesses.BasePriority}");
            }

            Process current = Process.GetCurrentProcess();
            Console.WriteLine($"ID: {current.Id} \nProcessName: {current.ProcessName} \nBasePriority: {current.BasePriority} \nStartTime: {current.StartTime} \nTotalProcessorTime: {current.TotalProcessorTime}");
            writer.WriteLine($"ID: {current.Id} \nProcessName: {current.ProcessName} \nBasePriority: {current.BasePriority} \nStartTime: {current.StartTime} \nTotalProcessorTime: {current.TotalProcessorTime}");
            writer.Close();
            Console.WriteLine();


            //2
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Имя домена: {domain.FriendlyName}");
            Console.WriteLine($"Конфигурации домена: {domain.SetupInformation}");
            Console.WriteLine($"Сборки домена: {domain.BaseDirectory}");
            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                Console.WriteLine(asm.GetName().Name);
            }
            Console.WriteLine();

            //3
            Console.WriteLine("Введите число n");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Счетчик до n");
            Thread mythread = new Thread(new ParameterizedThreadStart(Count));
            mythread.Start(x);

            Console.WriteLine($"Is thread alive? {mythread.IsAlive}");
            mythread.Name = "AnotherThread";
            Console.WriteLine($"Thread's name: {mythread.Name}");
            Console.WriteLine($"Thread's priority: {mythread.Priority}");
            Console.WriteLine($"Thread's state: {mythread.ThreadState}");
            Console.WriteLine($"ID: {Thread.GetDomainID()}");
            Thread.Sleep(5000);
            Console.WriteLine();

            //4
            Console.WriteLine("Два счетчика");
            int x1 = x;
            Thread mythread1 = new Thread(new ParameterizedThreadStart(Even));
            mythread1.Start(x1);
            Thread mythread2 = new Thread(new ParameterizedThreadStart(Odd));
            mythread2.Start(x1);
            Thread.Sleep(10000);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //5
            int num = 0;
            TimerCallback tm = new TimerCallback(Count1);
            Timer timer = new Timer(tm, num, 0, 2000);
        }
        public static void Count(object x)
        {
            int n = (int)x;
            StreamWriter writer = new StreamWriter(@"D:/AnotherThread.txt");
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(i);
                writer.WriteLine(i);
                Thread.Sleep(20);
            }
            writer.Close();
        }
        public static void Even(object x)
        {
            mutexObj.WaitOne();
            int n = (int)x;
            StreamWriter writer = new StreamWriter(@"D:/Threads.txt");

            for (int i = 1; i < n; i += 2)
            {
                Thread.Yield();
                Thread.Sleep(500);
                Console.WriteLine(i);
                writer.WriteLine(i);
                turn = true;

            }

            writer.Close();
            mutexObj.ReleaseMutex();
        }
        public static void Odd(object x)
        {
            mutexObj.WaitOne();
            int n = (int)x;
            StreamWriter writer = new StreamWriter(@"D:/Threads.txt");

            for (int i = 2; i < n; i += 2)
            {

                Thread.Sleep(100);
                Console.WriteLine(i);
                writer.WriteLine(i);
                turn = true;

            }

            writer.Close();
            mutexObj.ReleaseMutex();
        }
        public static void Count1(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }
    }
}
