using Silverfly;
using Silverfly.Nodes;
using Silverfly.Nodes.Operators;
using Silverfly.Parselets;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;

public class LabelRefParselet : IPrefixParselet
{
    public AstNode Parse(Parser parser, Token token)
    {
        var label = parser.Consume(PredefinedSymbols.Name);

        return new PrefixOperatorNode(token.Rewrite("$"), new NameNode(label))
            .WithRange(token)
            .WithTag("labelref");
    }
}