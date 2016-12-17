using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Queens.PC
{
    internal class Producer
    {
        private ConcurrentQueue<int[]> proposals;

        public Producer(ConcurrentQueue<int[]> queue)
        {
            proposals = queue;
        }

        public async Task<int> StartAsync(int size, int consumers)
        {
            await Task.Run(() => Produce(size));

            for (int i = 0; i < consumers; i++)
            {
                Put(new[] { 99 });
            }

            return 0;
        }

        public void Produce(int size)
        {
            Backtrack(0, new int[size]);
        }

        public void Put(int[] positions)
        {
            var narr = new int[positions.Length];

            Array.Copy(positions, narr, positions.Length);

            proposals.Enqueue(narr);
        }

        public void Backtrack(int row, int[] positions)
        {
            if (row == positions.Length)
            {
                Put(positions);
            }
            else
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[row] = i;
                    Backtrack(row + 1, positions);
                }
            }
        }
    }
}
