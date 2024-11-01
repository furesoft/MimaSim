using System.Collections.Generic;
using System.Collections.Immutable;
using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Contexts;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.High.Parselets;

public class FuncDefParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        using var context = parser.Lexer.OpenContext<FuncContext>();

        var nameToken = parser.Consume(PredefinedSymbols.Name);
        parser.Consume("(");
        var parameters = ParseArgs(parser, ",", ")");
        parser.Consume("{");
        var body = parser.ParseList(terminators: "}");

        return new FuncDefNode(nameToken, parameters, body)
            .WithRange(token, parser.LookAhead());
    }

    private ImmutableList<AstNode> ParseArgs(Parser parser, Symbol separator, params Symbol[] terminators)
    {
        var args = new List<AstNode>();

        do
        {
            var token = parser.Consume();

            if (token.Type == ")")
            {
                break;
            }
            args.Add(new NameNode(token));
        } while (parser.Match(separator) && parser.Lexer.IsNotAtEnd());

        parser.Match(terminators);

        return [.. args];
    }
}