using MimaSim.Core;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;

namespace MimaSim.MIMA.Parsing.SourceTranslators
{
    public class AssemblySourceTextTranslator : ISourceTextTranslator
    {
        public byte[] ToRaw(string input, out bool hasErrors)
        {
            var parser = new AssemblyParser();
            var ast = (CallNode)parser.Parse(input);

            if (ast.IsEmpty || ast.Type == null)
            {
                hasErrors = true;
                return new byte[0];
            }

            var visitor = new AssemblyVisitor();

            ast.Visit(visitor);

            hasErrors = false;

            return visitor.GetRaw();
        }
    }
}