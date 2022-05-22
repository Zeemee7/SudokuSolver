using SudokuSolver.simsoft;
using System.Reflection;

namespace SudokuSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISudoku problem = new CsvSudokuReader(new FileInfo("sudokus/mittel.txt")).Read();
            ISudokuUserInterface userInterface = new ConsoleSudokuUserInterface();
            userInterface.DisplaySudoku(problem);
            List<ISudokuSolver> sudokuSolvers = LoadSolvers();
            ISudokuSolver solver = userInterface.LetUserSelectSolver(sudokuSolvers);

            userInterface.DisplayStart(solver);
            DateTime startTime = DateTime.Now;
            ISudoku solution = solver.Solve(problem);
            TimeSpan duration = DateTime.Now - startTime;
            if (solution.IsComplete() && solution.IsValid())
            {
                userInterface.DisplaySuccess(solution, duration);
            } else
            {
                userInterface.DisplayFailure(solution, duration);
            }
        }

        private static List<ISudokuSolver> LoadSolvers()
        {
            List<ISudokuSolver> solvers = new List<ISudokuSolver>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type>? results = from type in assembly.GetTypes()
                                             where typeof(ISudokuSolver).IsAssignableFrom(type)
                                             select type;
                foreach (Type type in results)
                {
                    if (type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract)
                    {
                        ISudokuSolver? solver = Activator.CreateInstance(type) as ISudokuSolver;
                        if (solver != null) solvers.Add(solver);
                    }
                }
            }
            return solvers;
        }
    }
}
