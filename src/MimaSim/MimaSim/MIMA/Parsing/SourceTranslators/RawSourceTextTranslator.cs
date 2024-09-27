using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;

namespace MimaSim.MIMA.Parsing.SourceTranslators;

public class RawSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new RawParser();
        var ast = (CallNode)parser.Parse(input);

        if (ast.IsEmpty || ast.Type == null)
        {
            diagnostics.ReportUnknownError();
            return [];
        }

        var visitor = new RawParserVisitor();

        ast.Visit(visitor);

        return visitor.GetRaw();
    }
}