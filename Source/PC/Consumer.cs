using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Queens.PC
{
    internal class Consumer
    {
        private ConcurrentQueue<int[]> proposals;
        public Consumer(ConcurrentQueue<int[]> queue)
        {
            proposals = queue;
        }

        public async Task<int> Start()
        {
            var r = await Task.Run(() => Consume());
            return r;
        }

        public int[] Take()
        {
            int[] result;
            while (!proposals.TryDequeue(out result)) ;
            return result;
        }

        public int Consume()
        {
            int solutions = 0;
            while (true)
            {
                var result = Take();

                if (result.Length == 1)
                {
                    return solutions;
                }
                Boolean s = true;

                for (int i = 0; i < result.Length; i++)
                {
                    if (!IsValid(result, i))
                    {
                        s = false;
                        break;
                    }
                }

                if (s)
                {
                    solutions++;
                }
            }
        }

        public Boolean IsValid(int[] positions, int currentRow)
        {
            for (int j = 0; j < currentRow; j++)
            {
                if (positions[j] == positions[currentRow] ||
                    positions[j] == positions[currentRow] - (currentRow - j) ||
                    positions[j] == positions[currentRow] + (currentRow - j))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
