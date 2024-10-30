using MimaSim.Core.Parsing;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.Raw;

public class RawSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, out SourceDocument document)
    {
        var parser = new RawParser();
        var ast = parser.Parse(input);

        document = parser.Document;
        if (ast.Document.Messages.Count > 0)
        {
            return [];
        }

        var visitor = new RawParserVisitor();

        ast.Tree.Accept(visitor);

        return visitor.GetRaw();
    }
}