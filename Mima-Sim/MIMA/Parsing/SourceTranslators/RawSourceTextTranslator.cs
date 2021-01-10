using MimaSim.Core;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;

namespace MimaSim.MIMA.Parsing.SourceTranslators
{
    public class RawSourceTextTranslator : ISourceTextTranslator
    {
        public byte[] ToRaw(string input)
        {
            var parser = new RawParser();
            var ast = (CallNode)parser.Parse(input);
            var visitor = new RawParserVisitor();

            ast.Visit(visitor);

            return visitor.GetRaw();
        }
    }
}