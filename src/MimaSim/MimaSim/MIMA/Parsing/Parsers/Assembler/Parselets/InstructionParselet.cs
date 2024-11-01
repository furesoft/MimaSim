using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;

public class InstructionParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        if (parser.LookAhead().Type != ":")
        {
            if (!parser.Lexer.IsContext<InstructionContext>())
            {
                using var context = parser.Lexer.OpenContext<InstructionContext>();

                var args = parser.ParseSeperated(",", terminator: "\n");
                return new InstructionNode(token, args)
                    .WithRange(token, parser.LookAhead());
            }
        }

        return new NameParselet().Parse(parser, token);
    }
}