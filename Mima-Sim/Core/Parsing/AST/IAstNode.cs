namespace MimaSim.Core.Parsing.AST
{
    public interface IAstNode
    {
        string ToString();

        void Visit(INodeVisitor visitor);
    }
}