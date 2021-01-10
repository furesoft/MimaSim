using MimaSim.Core.AST.Nodes;

namespace MimaSim.Core.AST
{
    public interface INodeVisitor
    {
        void Visit(LiteralNode lit);

        void Visit(IdentifierNode id);

        void Visit(CallNode call);
    }
}