using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, out SourceDocument document)
    {
        var parser = new HighParser();
        var ast = parser.Parse(input);
        var visitor = new HighParserVisitor();

        ast.Tree.Accept(visitor);

        document = parser.Document;
        return visitor.GetRaw();
    }
}