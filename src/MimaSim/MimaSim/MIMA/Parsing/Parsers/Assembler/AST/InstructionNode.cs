using System.Collections.Immutable;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.AST;

public record InstructionNode(Token Mnemnonic, ImmutableList<AstNode> Args) : AstNode
{
}