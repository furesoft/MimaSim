using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core
{
    public class DiagnosticBag
    {
        private List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public bool IsEmpty => _diagnostics.Count == 0;

        public string[] GetAll()
        {
            return _diagnostics.Select(_ => _.ToString()).ToArray();
        }

        public void ReportInvalidMovInstruction(int start, int end)
        {
            Report("mov besitzt ein ungültiges Argument", start, end);
        }

        internal void ReportUnknownError()
        {
            Report("ein unbekannter Fehler ist aufgetreten", 0, 0);
        }

        private void Report(string message, int start, int end)
        {
            _diagnostics.Add(new Diagnostic { Message = message, Start = start, End = end });
        }

        public class Diagnostic
        {
            public int End { get; set; }
            public string Message { get; set; }
            public int Start { get; set; }

            public override string ToString()
            {
                return $"(Start:End {Start}:{End}): {Message}";
            }
        }
    }
}