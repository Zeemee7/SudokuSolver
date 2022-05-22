namespace SudokuSolver
{
    public class ConsoleSudokuUserInterface : ISudokuUserInterface
    {

        private const ConsoleColor NEUTRAL = ConsoleColor.Gray;
        private const ConsoleColor LINE_EMPHASIZED = ConsoleColor.Blue;
        private const ConsoleColor NUMBERS = ConsoleColor.Green;

        public void DisplaySudoku(ISudoku sudoku)
        {
            WriteRowDivider(true);
            for (byte r = 1; r < 10; r++)
            {
                WriteRow(sudoku, r);
                WriteRowDivider(r == 3 || r == 6 || r == 9);
            }
            //Console.ReadKey();
        }

        public ISudokuSolver LetUserSelectSolver(List<ISudokuSolver> sudokuSolvers)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nFolgende Implementierungen von ISudokuSolver wurden gefunden:");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i <= sudokuSolvers.Count; i++)
            {
                Console.Write("(" + i + ") ");
                Console.WriteLine(sudokuSolvers[i - 1].ToString());
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWelcher soll verwendet werden? (Taste 1-" + sudokuSolvers.Count + ")");
                Console.ForegroundColor = ConsoleColor.Red;
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                Console.ForegroundColor = NEUTRAL;
                if (int.TryParse(consoleKeyInfo.KeyChar.ToString(), out int selectedNo))
                {
                    if (selectedNo > 0 && selectedNo <= sudokuSolvers.Count)
                    {
                        return sudokuSolvers[selectedNo - 1];
                    }
                }
            }
        }

        public void DisplayResult(ISudoku problem, ISudoku solution, TimeSpan duration)
        {
            Console.WriteLine("\n\nLösungsfindung beendet. Aktueller Stand:");
            DisplaySudoku(solution);
            bool complete = solution.IsComplete();
            bool valid = solution.IsValid();
            bool matches = solution.Matches(problem);
            if (complete && valid && matches)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nERFOLG!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMISSERFOLG!");
            }
            Console.ForegroundColor = NEUTRAL;
            WriteResultLine("Komplett", complete);
            WriteResultLine("Korrekt", valid);
            WriteResultLine("Ehrlich", matches);
            Console.WriteLine("Benötigte Zeit:\t" + duration);
        }

        private static void WriteResultLine(string name, bool result)
        {
            Console.Write(name + "?\t");
            string jn = result ? "Ja" : "Nein";
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(jn);
            Console.ForegroundColor = NEUTRAL;
        }

        public void DisplayStart(ISudokuSolver solver)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nStarte Lösungsfindung mit " + solver + " um " + DateTime.Now + "...");
            Console.ForegroundColor = NEUTRAL;
        }

        private static void WriteRow(ISudoku sudoko, byte lineNo)
        {
            Console.ForegroundColor = LINE_EMPHASIZED;
            Console.Write("|");
            Console.ForegroundColor = NEUTRAL;
            for (byte c = 1; c < 10; c++)
            {
                byte value = sudoko.GetValue(lineNo, c);
                Console.ForegroundColor = NUMBERS;
                Console.Write(" " + (value > 0 ? value : " ") + " ");
                ConsoleColor lineColor = c == 3 || c == 6 || c == 9 ? LINE_EMPHASIZED : NEUTRAL;
                WriteColumnDivider(lineColor);
            }
            Console.WriteLine();
        }

        private static void WriteColumnDivider(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("|");
            Console.ForegroundColor = NEUTRAL;
        }

        private static void WriteRowDivider(bool emphasized)
        {
            Console.ForegroundColor = LINE_EMPHASIZED;
            Console.Write("+");
            if (emphasized)
            {
                Console.Write("---+---+---+---+---+---+---+---+---+");
            }
            else
            {
                for (byte i = 0; i < 3; i++)
                {
                    Console.ForegroundColor = NEUTRAL;
                    Console.Write("---+---+---");
                    Console.ForegroundColor = LINE_EMPHASIZED;
                    Console.Write("+");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = NEUTRAL;
        }
    }
}
