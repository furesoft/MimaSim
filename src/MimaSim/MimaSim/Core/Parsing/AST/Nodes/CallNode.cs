using System.Collections.Generic;
using System.Linq;
using MimaSim.MIMA.Parsing;

namespace MimaSim.Core.Parsing.AST.Nodes;

public struct CallNode(AstCallNodeType type, List<IAstNode> args) : IAstNode
{
    public List<IAstNode> Args { get; set; } = args;

    public bool IsEmpty => Args.Count == 0;

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