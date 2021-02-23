namespace MimaSim.Core.AST
{
    public interface IAstNode
    {
        string ToString();

        void Visit(INodeVisitor visitor);
    }
}