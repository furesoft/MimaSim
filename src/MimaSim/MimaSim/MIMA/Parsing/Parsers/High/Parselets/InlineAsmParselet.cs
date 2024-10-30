using MimaSim.MIMA.Parsing.Parsers.High.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.High.Parselets;

public class InlineAsmParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        var def = parser.Consume(PredefinedSymbols.String);

        return new AsmNode(def.Text.ToString());
    }
}