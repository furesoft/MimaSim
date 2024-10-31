using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Contexts;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High.Parselets;

public class ReturnParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        if (!parser.Lexer.IsContext<FuncContext>())
        {
            parser.Document.AddMessage(MessageSeverity.Error, "Return is only allowed in a function", token.Line, token.Column, parser.LookAhead().Line, parser.LookAhead().Column);
        }

        AstNode? value = null;
        if (!parser.IsMatch(";"))
        {
            value = parser.ParseExpression();
        }

        parser.Consume(";");

        return new ReturnStatement(value);
    }
}