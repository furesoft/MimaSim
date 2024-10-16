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
        var parameters = parser.ParseList(terminators: ")");
        parser.Consume("{");
        var body = parser.ParseList(terminators: "}");

        return new MacroNode(nameToken, parameters, body);
    }
}