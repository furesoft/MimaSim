using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;

public class MacroInvocationParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        if (!parser.Lexer.IsContext<InstructionContext>())
        {
            var args = parser.ParseSeperated(",");

            return new MacroInvocationNode(token, args)
                .WithRange(token, parser.LookAhead(0));
        }

        return new NameNode(token).WithRange(token);
    }
}