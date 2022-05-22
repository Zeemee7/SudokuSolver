using System.Text.RegularExpressions;

namespace SudokuSolver
{
    public class CsvSudokuReader : ISudokuReader
    {

        private readonly FileInfo file;
        private const string formatPattern = "(^|;)\\s*([1-9])([1-9])([1-9])\\s*";

        public CsvSudokuReader(FileInfo file)
        {
            this.file = file;
        }

        public ISudoku Read()
        {
            if (!file.Exists) throw new FileNotFoundException("Datei " + file.FullName + " existiert nicht!");
            string content = File.ReadAllText(file.FullName);
            MatchCollection matchCollection = Regex.Matches(content, formatPattern);
            if (matchCollection.Count == 0) throw new ArgumentException("Ungültiges Format! Erwartet: zsw;zsw;zsw... -> z: Zeilennummer 1-9, s: Spaltennummer 1-9; w: Wert 1-9");
            ArraySudoku sudoku = new ArraySudoku();
            foreach (Match match in matchCollection)
            {
                sudoku.SetValue(ParseMatch(match, 2), ParseMatch(match, 3), ParseMatch(match, 4));
            }
            return sudoku;
        }

        private static byte ParseMatch(Match match, int groupNo)
        {
            return byte.Parse(match.Groups[groupNo].Value);
        }
    }
}
