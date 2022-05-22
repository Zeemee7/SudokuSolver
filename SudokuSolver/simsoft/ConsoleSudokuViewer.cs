namespace SudokuSolver.simsoft
{
    public class ConsoleSudokuViewer : ISudokuViewer
    {

        private const ConsoleColor LINE_NEUTRAL = ConsoleColor.Gray;
        private const ConsoleColor LINE_EMPHASIZED = ConsoleColor.Blue;
        private const ConsoleColor NUMBERS = ConsoleColor.Green;

        public void View(ISudoku sudoku)
        {
            WriteRowDivider(true);
            for (byte r = 1; r < 10; r++)
            {
                WriteRow(sudoku, r);
                WriteRowDivider(r == 3 || r == 6 || r == 9);
            }
            //Console.ReadKey();
        }

        private static void WriteRow(ISudoku sudoko, byte lineNo)
        {
            Console.ForegroundColor = LINE_EMPHASIZED;
            Console.Write("|");
            Console.ForegroundColor = LINE_NEUTRAL;
            for (byte c = 1; c < 10; c++)
            {
                byte value = sudoko.ValueAt(lineNo, c);
                Console.ForegroundColor = NUMBERS;
                Console.Write(" " + (value > 0 ? value : " ") + " ");
                ConsoleColor lineColor = (c == 3 || c == 6 || c == 9) ? LINE_EMPHASIZED : LINE_NEUTRAL;
                WriteColumnDivider(lineColor);
            }
            Console.WriteLine();
        }

        private static void WriteColumnDivider(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("|");
            Console.ForegroundColor = LINE_NEUTRAL;
        }

        private static void WriteRowDivider(bool emphasized)
        {
            Console.ForegroundColor = LINE_EMPHASIZED;
            Console.Write("+");
            if (emphasized)
            {
                Console.Write("---+---+---+---+---+---+---+---+---+");
            } else
            {
                for (byte i = 0; i < 3; i++)
                {
                    Console.ForegroundColor = LINE_NEUTRAL;
                    Console.Write("---+---+---");
                    Console.ForegroundColor = LINE_EMPHASIZED;
                    Console.Write("+");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = LINE_NEUTRAL;
        }
    }
}
