using SudokuSolver.simsoft;

namespace SudokuSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISudoku problem = new CsvSudokuReader(new FileInfo("sudokus/mittel.txt")).Read();
            new ConsoleSudokuViewer().View(problem);
            //ISudoku solution = new BruteForceSudokuSolver().Solve(problem);
            //new ConsoleSudokuViewer().View(solution);
        }
    }
}
