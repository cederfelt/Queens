using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Queens.PC;

namespace Queens
{
    internal class ProducerConsumerSolver
    {
        private readonly Producer _prod;
        private readonly Consumer _cons;

        private readonly ConcurrentQueue<int[]> proposals;

        public ProducerConsumerSolver()
        {
            proposals = new ConcurrentQueue<int[]>();
            _prod = new Producer(proposals);
            _cons = new Consumer(proposals);
        }

        public async Task<int> StartAsync(int nrProducers = 1, int nrConsumers = 1, int boardSize = 8)
        {
            Task<int>[] consumerTask = new Task<int>[nrProducers + nrConsumers];
            int i = 0;
            int j;
            for (j = 0; j < nrProducers; j++)
            {
                consumerTask[j] = _prod.StartAsync(boardSize, nrConsumers);
            }

            for (i = 0; i < nrConsumers; i++)
            {
                for (i = 0; i < nrConsumers; i++)
                {
                    consumerTask[i + j] = _cons.Start();
                }
            }

            var results = await Task.WhenAll(consumerTask);

            var solutions = results.Sum();

            return solutions;
        }
    }
}
