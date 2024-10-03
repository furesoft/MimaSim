using System.Collections.Immutable;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler.AST;

public record InstructionNode(Mnemnonics Mnemnonic, ImmutableList<AstNode> Args) : AstNode
{
    
}