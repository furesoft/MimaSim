using MimaSim.Core.Parsing.Tokenizer;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core;

public class DiagnosticBag
{
    private readonly List<Diagnostic> _diagnostics = [];

    public bool IsEmpty => _diagnostics.Count == 0;

    public string[] GetAll()
    {
        return _diagnostics.Select(_ => _.ToString()).ToArray();
    }

    public void ReportHexLiteralExpected(Token token)
    {
        Report($"Hexadezimalzahl erwartet. Bekommen '{token.Contents}'", token.Start, token.End);
    }

    public void ReportInvalidLiteral(Token token)
    {
        Report($"Ungültiges Literal '{token.Contents}'", token.Start, token.End);
    }

    public void ReportInvalidMovInstruction(int start, int end)
    {
        Report("mov besitzt ein ungültiges Argument", start, end);
    }

    public void ReportInvalidSymbol(Token lookahead)
    {
        Report($"Unerwartetes Symbol '{lookahead.Contents}'", lookahead.Start, lookahead.End);
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