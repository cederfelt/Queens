using System;
using System.Threading.Tasks;

namespace Queens
{
    public class Solver
    {

        public int SolveUnThreaded(int row, int[] positions)
        {
            int solutions = 0;
            solutions = Backtrack(row, positions, solutions);
            return solutions;
        }

        public async Task<int> SolveThreaded(int row, int[] positions)
        {
            int solutions = 0;
            solutions = await Task.Run(() => Backtrack(row, positions, solutions));
            return solutions;
        }

        public int Backtrack(int row, int[] positions, int solutions)
        {
            if (row == positions.Length)
            {
                solutions++;
                return solutions;
            }
            else
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[row] = i;
                    if (IsValid(positions, row))
                    {
                        solutions = Backtrack(row + 1, positions, solutions);
                    }
                }
            }

            return solutions;
        }

        public Boolean IsValid(int[] positions, int currentRow)
        {
            for (int j = 0; j < currentRow; j++)
            {
                if (positions[j] == positions[currentRow] || positions[j] == positions[currentRow] - (currentRow - j) ||
                    positions[j] == positions[currentRow] + (currentRow - j))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
