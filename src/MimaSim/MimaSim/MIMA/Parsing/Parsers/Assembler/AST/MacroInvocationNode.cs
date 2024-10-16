using System.Collections.Immutable;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.AST;

public record MacroInvocationNode(Token NameToken, ImmutableList<AstNode> Arguments) : AstNode
{
    
}