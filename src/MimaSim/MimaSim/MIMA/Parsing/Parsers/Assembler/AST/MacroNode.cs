using System.Collections.Immutable;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.AST;

public record MacroNode(Token NameToken, ImmutableList<AstNode> Parameters, ImmutableList<AstNode> Body) : AstNode
{

}