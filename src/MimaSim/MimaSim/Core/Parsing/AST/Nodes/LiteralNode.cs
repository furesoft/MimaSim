namespace MimaSim.Core.Parsing.AST.Nodes;

public struct LiteralNode(object? value) : IAstNode
{
    public object? Value { get; set; } = value;

    public override string ToString()
    {
        return Printer.Default.Print(this);
    }

    public void Visit(INodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}