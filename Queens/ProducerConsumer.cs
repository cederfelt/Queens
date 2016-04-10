
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;
using Queens.PC;

namespace Queens
{
    class ProducerConsumer
    {

        private readonly Producer _prod;
        private readonly Consumer _cons;

        private readonly ConcurrentQueue<int[]> proposals;

        public ProducerConsumer()
        {
            proposals = new ConcurrentQueue<int[]>();
            _prod = new Producer(proposals);
            _cons = new Consumer(proposals);
        }

        public async Task<int> Start(int nrProducers = 1, int nrConsumers = 1, int size = 8)
        {
            Task[] consumerTask = new Task[nrProducers + nrConsumers];
            int i = 0;
            int j;
            for (j = 0; j < nrProducers; j++)
            {
                consumerTask[j] = _prod.Start(size, nrConsumers);
            }

            for (i = 0; i < nrConsumers; i++)
            {
                for (i = 0; i < nrConsumers; i++)
                {
                    consumerTask[i + j] = _cons.Start();
                }
            }

            await Task.WhenAll(consumerTask);
            
            var solutions = consumerTask.Sum(task => ((Task<int>)task).Result);

            return solutions;
        }
    }
}
