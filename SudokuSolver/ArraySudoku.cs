namespace SudokuSolver
{
    public class ArraySudoku : ISudoku
    {
        readonly byte[,] fields = new byte[9, 9];

        public ArraySudoku() { }

        private ArraySudoku(byte[,] fields)
        {
            this.fields = fields;
        }

        public byte GetValue(byte row, byte col)
        {
            if (row < 1 || row > 9) throw new ArgumentOutOfRangeException();
            if (col < 1 || col > 9) throw new ArgumentOutOfRangeException();
            return fields[row - 1, col - 1];
        }

        public void SetValue(byte row, byte col, byte value)
        {
            if (row < 1 || row > 9) throw new ArgumentOutOfRangeException();
            if (col < 1 || col > 9) throw new ArgumentOutOfRangeException();
            if (value < 1 || value > 9) throw new ArgumentOutOfRangeException();
            fields[row - 1, col - 1] = value;
        }

        public bool IsComplete()
        {
            for (byte ri = 0; ri < 9; ri++)
            {
                for (byte ci = 0; ci < 9; ci++)
                {
                    if (fields[ci, ri] == 0) return false;
                }
            }
            return true;
        }

        public bool IsValid()
        {
            for (byte i = 0; i < 9; i++)
            {
                if (!IsRowValid(i)) return false;
                if (!IsColumnValid(i)) return false;
            }
            for (byte ri = 0; ri < 9; ri += 3)
            {
                for (byte ci = 0; ci < 9; ci += 3)
                {
                    if (!IsBoxValid(ri, ci)) return false;
                }
            }
            return true;
        }

        public bool OriginatesFrom(ISudoku otherSudoku)
        {
            for (byte r = 1; r < 10; r++)
            {
                for (byte c = 1; c < 10; c++)
                {
                    byte original = otherSudoku.GetValue(r, c);
                    if (original != 0 && GetValue(r, c) != original) return false;
                }
            }
            return true;
        }

        public bool Matches(ISudoku otherSudoku)
        {
            for (byte r = 1; r < 10; r++)
            {
                for (byte c = 1; c < 10; c++)
                {
                    byte original = otherSudoku.GetValue(r, c);
                    if (GetValue(r, c) != original) return false;
                }
            }
            return true;
        }

        public ISudoku Clone()
        {
            return new ArraySudoku((byte[,])fields.Clone());
        }

        private bool IsRowValid(byte i)
        {
            return AreNumbersUnique(fields[i, 0], fields[i, 1], fields[i, 2], fields[i, 3], fields[i, 4], fields[i, 5], fields[i, 6], fields[i, 7], fields[i, 8]);
        }

        private bool IsColumnValid(byte i)
        {
            return AreNumbersUnique(fields[0, i], fields[1, i], fields[2, i], fields[3, i], fields[4, i], fields[5, i], fields[6, i], fields[7, i], fields[8, i]);
        }

        private bool IsBoxValid(byte ri, byte ci)
        {
            return AreNumbersUnique(fields[ri, ci], fields[ri, ci + 1], fields[ri, ci + 2], fields[ri + 1, ci], fields[ri + 1, ci + 1], fields[ri + 1, ci + 2], fields[ri + 2, ci], fields[ri + 2, ci + 1], fields[ri + 2, ci + 2]);
        }

        private static bool AreNumbersUnique(params byte[] numbers)
        {
            if (numbers.Length > 9) throw new ArgumentOutOfRangeException("numbers darf nicht mehr als 9 Elemente enthalten!");
            // Wird mit false initialisiert
            bool[] check = new bool[9];
            foreach (byte n in numbers)
            {
                if (n < 0 || n > 9) throw new ArgumentOutOfRangeException("Elemente von numbers müssen zwischen 0 und 9 sein.");
                if (n == 0) continue; // 0s werden ignoriert
                // Schon gefunden?
                if (check[n - 1]) return false;
                // Setze auf true, damit wird der Fund markiert
                check[n - 1] = true;
            }
            return true;
        }
    }
}
