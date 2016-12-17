using System;
using System.Threading.Tasks;

namespace Queens
{
    //Solves by bruteforce. Trying to place a queen in the first column for each row, checks if the board is valid, if not move a queen.
    class BruteforceSolver
    {

        public int SolveUnThreaded(int row, int[] positions)
        {
            int solutions = 0;
            solutions = Backtrack(row, positions, solutions);
            return solutions;
        }

        public async Task<int> SolveThreadedAsync(int row, int[] positions)
        {
            int solutions = 0;
            solutions = await Task.Run(() => Backtrack(row, positions, solutions));
            return solutions;
        }

        public int Backtrack(int row, int[] positions, int solutions)
        {
            if (row == positions.Length)
            {
                Boolean validBoard = true;

                for (int i = 0; i < positions.Length; i++)
                {
                    if (!IsValid(positions, i))
                    {
                        validBoard = false;
                        break;
                    }
                }
                if (validBoard)
                {
                    solutions++;
                }

                return solutions;
            }
            else
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[row] = i;
                    solutions = Backtrack(row + 1, positions, solutions);
                }
            }

            return solutions;
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
