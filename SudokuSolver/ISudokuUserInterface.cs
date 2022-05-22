namespace SudokuSolver
{
    public interface ISudokuUserInterface
    {
        void DisplaySudoku(ISudoku sudoku);

        ISudokuSolver LetUserSelectSolver(List<ISudokuSolver> sudokuSolvers);

        void DisplayResult(ISudoku problem, ISudoku solution, TimeSpan duration);

        void DisplayStart(ISudokuSolver solver);
    }
}
