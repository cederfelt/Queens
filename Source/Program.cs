using System;
using System.Linq;
using System.Threading.Tasks;

namespace Queens
{
    internal class Program
    {
        private const int DefaultBoardDimensions = 8; 
        //A lot faster to produce the boards than consuming them.
        private const int NrConsumers = 4;
        private const int NrProducers = 1;

        static void Main()
        {
            Console.WriteLine($"Input size, if blank it will be {DefaultBoardDimensions}");
            if (!int.TryParse(Console.ReadLine(), out int boardDimensions))
            {
                boardDimensions = DefaultBoardDimensions;
            }

            //Don't know of a better way than .wait for console application.
            Console.WriteLine("--------------------------------------------");
            SmarterNotThreaded(boardDimensions);
            Task.Run(() => SmarterThreadedAsync(boardDimensions)).Wait();
            Console.WriteLine("--------------------------------------------");
            BruteforceNotThreaded(boardDimensions);
            Task.Run(() => BruteforceThreadedAsync(boardDimensions)).Wait();
            Console.WriteLine("--------------------------------------------");
            Task.Run(() => ProducerConsumerMethodAsync(DefaultBoardDimensions)).Wait();

            Console.ReadLine();
        }

        private static async Task ProducerConsumerMethodAsync(int sizes)
        {
            ProducerConsumerSolver pc = new ProducerConsumerSolver();
            int solutions;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            solutions = await pc.StartAsync(nrConsumers: NrConsumers, nrProducers: NrProducers, boardSize: sizes);

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Producer Consumer:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static async Task SmarterThreadedAsync(int size)
        {
            var taskPositions = new int[size][];
            int solutions;
            Task<int>[] tList;

            SmarterSolver s = new SmarterSolver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //place the first row queens in preparation for the threads
            for (int i = 0; i < taskPositions.Length; i++)
            {
                taskPositions[i] = new int[size];
                taskPositions[i][0] = i;
            }

            //Create all tasks and place them in the array
            tList = taskPositions.Select((t, i) => i).Select(i1 => s.SolveThreadedAsync(1, taskPositions[i1])).Cast<Task<int>>().ToArray();

            var results = await Task.WhenAll(tList);
            solutions = results.Sum();

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

        private static async Task BruteforceThreadedAsync(int size)
        {
            var taskPositions = new int[size][];
            int solutions;
            Task<int>[] tList;

            BruteforceSolver s = new BruteforceSolver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //place the first row queens in preparation for the threads
            for (int i = 0; i < taskPositions.Length; i++)
            {
                taskPositions[i] = new int[size];
                taskPositions[i][0] = i;
            }
            //Create all tasks and place them in the array
            tList = taskPositions.Select((t, i) => i).Select(i1 => s.SolveThreadedAsync(1, taskPositions[i1])).Cast<Task<int>>().ToArray();

            var results = await Task.WhenAll(tList);
            solutions = results.Sum();

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Bruteforce Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }

        private static void BruteforceNotThreaded(int size)
        {
            var positions = new int[size];
            int solutions;
            BruteforceSolver s = new BruteforceSolver();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            solutions = s.SolveUnThreaded(0, positions);

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Bruteforce Not Threaded:");
            Console.WriteLine($"Solutions {solutions}");
            Console.WriteLine($"Elapsed milliseconds {elapsedMs}");
        }
    }
}
