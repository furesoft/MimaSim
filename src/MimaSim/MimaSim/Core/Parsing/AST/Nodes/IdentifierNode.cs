namespace MimaSim.Core.Parsing.AST.Nodes;

public struct IdentifierNode(string name) : IAstNode
{
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return Printer.Default.Print(this);
    }

    public void Visit(INodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}