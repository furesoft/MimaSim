using System;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, out SourceDocument document)
    {
        var parser = new HighParser();
        var ast = parser.Parse(input);

        var preparationVisitor = new PreparationVisitor();

        if (preparationVisitor.SymbolMap.Get("main") is not FunctionSymbol)
        {
            parser.Document.AddMessage(MessageSeverity.Error, "Main function not found", SourceRange.Empty);

            document = parser.Document;
            return [];
        }

        ast.Tree.Accept(preparationVisitor);

        var visitor = new HighParserVisitor();
        ast.Tree.Accept(visitor, preparationVisitor.SymbolMap);

        document = parser.Document;

        return visitor.GetRaw();
    }
}