using MimaSim.MIMA.Parsing;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Parsing.AST.Nodes;

public struct CallNode(AstCallNodeType type, List<IAstNode> args) : IAstNode
{
    public List<IAstNode> Args { get; set; } = args;

    public bool IsEmpty => !Args.Any();

    public AstCallNodeType Type { get; set; } = type;

    public override string ToString()
    {
        return Printer.Default.Print(this);
    }

    public void Visit(INodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}