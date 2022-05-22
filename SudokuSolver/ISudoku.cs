namespace SudokuSolver
{
    public interface ISudoku
    {
        bool IsComplete();
        bool IsValid();
        void SetValue(byte row, byte col, byte value);
        byte GetValue(byte row, byte col);
    }
}
