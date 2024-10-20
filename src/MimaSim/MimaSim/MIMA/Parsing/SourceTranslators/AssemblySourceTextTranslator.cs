using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.Assembler;

namespace MimaSim.MIMA.Parsing.SourceTranslators;

public class AssemblySourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new AssemblyParser();
        var ast = parser.Parse(input);

        var visitor = new AssemblyVisitor();
        ast.Tree.Accept(visitor, Scope.Root);

        return visitor.GetRaw();
    }
}