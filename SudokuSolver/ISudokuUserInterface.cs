namespace SudokuSolver
{
    public interface ISudokuUserInterface
    {
        void DisplaySudoku(ISudoku sudoku);

        ISudokuSolver LetUserSelectSolver(List<ISudokuSolver> sudokuSolvers);

        void DisplayFailure(ISudoku solution, TimeSpan duration);

        void DisplaySuccess(ISudoku solution, TimeSpan duration);

        void DisplayStart(ISudokuSolver solver);
    }
}
