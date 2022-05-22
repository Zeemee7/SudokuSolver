namespace SudokuSolver
{
    public interface ISudoku
    {
        bool Solved
        {
            get;
        }

        byte ValueAt(byte row, byte col);
    }
}
