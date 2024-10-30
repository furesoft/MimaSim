using System.Diagnostics;
using MimaSim.Core;
using MimaSim.Core.Parsing;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblySourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, out Silverfly.Text.SourceDocument document)
    {
        var parser = new AssemblyParser();

        parser.Lexer.SetSource(input);
        while (parser.Lexer.IsNotAtEnd())
        {
            var token = parser.Lexer.Next().ToString();

            if (token == "\n")
            {
                token = "\\n";
            }

            Debug.WriteLine(token);
        }
        var ast = parser.Parse(input);

        var visitor = new AssemblyVisitor();
        ast.Tree.Accept(visitor, Scope.Root);

        document = parser.Document;
        return visitor.GetRaw();
    }
}