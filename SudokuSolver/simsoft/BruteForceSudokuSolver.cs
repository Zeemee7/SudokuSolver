using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.simsoft
{
    public class BruteForceSudokuSolver : ISudokuSolver
    {
        public ISudoku Solve(ISudoku problem)
        {

            return SolvePart(1, 1, problem);
        }

        private ISudoku SolvePart(byte r, byte c, ISudoku state)
        {
            while (state.GetValue(r, c) != 0)
            {
                // Nächstes Feld
                c++;
                if (c > 9)
                {
                    c = 1;
                    r++;
                }
                // Es gibt kein nächstes Feld mehr, wir sind durch!
                if (r > 9) return state;
            }
            ISudoku newState = state.Clone();
            for (byte v = 1; v < 10; v++)
            {
                newState.SetValue(r, c, v);
                if (newState.IsValid())
                {
                    ISudoku result = SolvePart(r, c, newState);
                    // Wurde beendet oder ein weiterer valider Weg gefunden?
                    if (result.IsComplete() || !result.Matches(newState))
                    {
                        return result;
                    }
                    // Ansonsten versuche es ab da nochmal
                }
            }
            // Kein Wert geht mehr, Kommando zurück
            return state;
        }
    }
}
