using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.simsoft
{
    internal class CheatingSudokuSolver : ISudokuSolver
    {
        public ISudoku Solve(ISudoku problem)
        {
            ISudoku solution = new ArraySudoku();
            byte[] startValues = { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
            for (byte r = 1; r < 10; r++)
            {
                byte v = startValues[r - 1];
                for (byte c = 1; c < 10; c++)
                {
                    solution.SetValue(r, c, v);
                    v++;
                    if (v > 9) v = 1;
                }
            }
            return solution;
        }
    }
}
