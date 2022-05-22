namespace SudokuSolver.Noggelsoft;

public class NoggelstratsSolver : ISudokuSolver
{
    public ISudoku Solve(ISudoku problem)
    {
        problem.SetValue(1,9,1);
        new ConsoleSudokuUserInterface().DisplaySudoku(problem);
        return problem;
    }
}