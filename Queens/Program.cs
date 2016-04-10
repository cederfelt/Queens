using System;
using System.Linq;
using System.Threading.Tasks;

namespace Queens
{
    class Program
    {
        private const int Defaultsize = 8;

        static void Main()
        {
            Console.WriteLine($"Input size, if blank it will be {Defaultsize}");

            int size;

            if (!int.TryParse(Console.ReadLine(), out size))
            {
                size = Defaultsize;
            }

            //Don't know of a better way than .wait for console application.
            Console.WriteLine("--------------------------------------------");
            SmarterNotThreaded(size);
            Task.Run(() => SmarterThreaded(size)).Wait();
            Console.WriteLine("--------------------------------------------");
            Task.Run(() => Threaded(size)).Wait();
            NotThreaded(size);
            //Uncomment to run, takes some more memory and cpu usage
           // Task.Run(() => ProducerConsumerMethod(size)).Wait();

            Console.ReadLine();
        }

        private static async Task ProducerConsumerMethod(int sizes)
        {
            ProducerConsumer pc = new ProducerConsumer();
            int solutions;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            solutions = await pc.Start(nrConsumers: 4, size: sizes);

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Producer Consumer:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static async Task SmarterThreaded(int size)
        {
            var taskPositions = new int[size][];
            int solutions;
            Task[] tList;

            SmarterSolver s = new SmarterSolver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < taskPositions.Length; i++)
            {
                taskPositions[i] = new int[size];
                taskPositions[i][0] = i;
            }

            tList = taskPositions.Select((t, i) => i).Select(i1 => s.SolveThreaded(1, taskPositions[i1])).Cast<Task>().ToArray();

            await Task.WhenAll(tList);
            solutions = tList.Sum(task => ((Task<int>)task).Result);

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("SmarterSolver Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static void SmarterNotThreaded(int size)
        {

            var positions = new int[size];
            int solutions;
            SmarterSolver s = new SmarterSolver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            solutions = s.SolveUnThreaded(0, positions);

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("SmarterSolver Not Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static async Task Threaded(int size)
        {
            var taskPositions = new int[size][];
            int solutions;
            Task[] tList;

            Solver s = new Solver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < taskPositions.Length; i++)
            {
                taskPositions[i] = new int[size];
                taskPositions[i][0] = i;
            }

            tList = taskPositions.Select((t, i) => i).Select(i1 => s.SolveThreaded(1, taskPositions[i1])).Cast<Task>().ToArray();

            await Task.WhenAll(tList);
            solutions = tList.Sum(task => ((Task<int>)task).Result);

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static void NotThreaded(int size)
        {

            var positions = new int[size];
            int solutions;
            Solver s = new Solver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            solutions = s.SolveUnThreaded(0, positions);

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Not Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }
    }
}
