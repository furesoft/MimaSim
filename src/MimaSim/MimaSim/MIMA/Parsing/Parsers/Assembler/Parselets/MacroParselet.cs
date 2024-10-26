using System.Collections.Generic;
using System.Collections.Immutable;
using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;

public class MacroParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        //macro setPixel(%x, %y) { load 0x01 }
        var nameToken = parser.Consume(PredefinedSymbols.Name);
        parser.Consume("(");
        var parameters = ParseArgs(parser, ",", ")");
        parser.Consume("{");
        var body = parser.ParseList(terminators: "}");

        return new MacroNode(nameToken, parameters, body)
            .WithRange(token, parser.LookAhead());
    }

    public ImmutableList<AstNode> ParseArgs(Parser parser, Symbol separator, params Symbol[] terminators)
    {
        var args = new List<AstNode>();

        do
        {
            var node = parser.Consume();

            args.Add(new NameNode(node));
        } while (parser.Match(separator) && parser.Lexer.IsNotAtEnd());

        parser.Match(terminators);

        return [.. args];
    }
}