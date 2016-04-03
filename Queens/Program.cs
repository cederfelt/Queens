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

            Console.WriteLine("--------------------------------------------");
            SmarterNotThreaded(size);
            SmarterThreaded(size);
            Console.WriteLine("--------------------------------------------");
            Threaded(size);
            NotThreaded(size);
            //Uncomment to run, takes some more memory and cpu usage
            //ProducerConsumerMethod(size);

            Console.ReadLine();
        }

        private static void ProducerConsumerMethod(int sizes)
        {
            ProducerConsumer pc = new ProducerConsumer();
            int solutions;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            solutions = pc.Start(nrConsumers: 4, size: sizes);

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Producer Consumer:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static void SmarterThreaded(int size)
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

            Task.WaitAll(tList);
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

        private static void Threaded(int size)
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

            Task.WaitAll(tList);
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
