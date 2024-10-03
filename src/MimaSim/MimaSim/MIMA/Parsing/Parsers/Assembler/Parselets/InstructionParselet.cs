using System;
using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;

public class InstructionParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        var args = parser.ParseSeperated(",");

        return new InstructionNode(Enum.Parse<Mnemnonics>(token.Text.Span, true), args)
            .WithRange(token, parser.LookAhead(0));
    }
}