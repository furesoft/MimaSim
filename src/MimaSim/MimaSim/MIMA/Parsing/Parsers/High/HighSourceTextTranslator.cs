using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Core.Parsing;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new HighParser();
        var ast = parser.Parse(input);

        if (parser.Diagnostics.IsEmpty)
        {
            var visitor = new HighParserVisitor();

            ast.Visit(visitor);

            return visitor.GetRaw();
        }

        DialogService.OpenError(string.Join('\n', parser.Diagnostics.GetAll()));
        diagnostics = parser.Diagnostics;

        return [];
    }
}