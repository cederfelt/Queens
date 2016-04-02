using System;
using System.Linq;
using System.Threading.Tasks;

namespace Queens
{
    class Program
    {
        private const int Defaultsize = 14;

        static void Main()
        {
            Console.WriteLine($"Input size, if blank it will be {Defaultsize}");

            int size;

            if (!int.TryParse(Console.ReadLine(), out size))
            {
                size = Defaultsize;
            }

            Threaded(size);
            NotThreaded(size);
            Console.ReadLine();
        }

        private static void Threaded(int size)
        {
            var taskPositions = new int[size][];
            int solutions;

            Solver s = new Solver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < taskPositions.Length; i++)
            {
                taskPositions[i] = new int[size];
                taskPositions[i][0] = i;
            }

            var tList = taskPositions.Select((t, i) => i).Select(i1 => s.SolveThreaded(1, taskPositions[i1])).Cast<Task>().ToList();
            Task.WaitAll(tList.ToArray());
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
