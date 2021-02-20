using MimaSim.Core;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;

namespace MimaSim.MIMA.Parsing.SourceTranslators
{
    public class AssemblySourceTextTranslator : ISourceTextTranslator
    {
        public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
        {
            var parser = new AssemblyParser();
            var ast = (CallNode)parser.Parse(input);

            if (ast.IsEmpty || ast.Type == null)
            {
                diagnostics.ReportUnknownError();

                return new byte[0];
            }

            var visitor = new AssemblyVisitor();

            ast.Visit(visitor);

            return visitor.GetRaw();
        }
    }
}