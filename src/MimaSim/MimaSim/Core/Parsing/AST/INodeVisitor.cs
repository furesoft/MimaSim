using MimaSim.Core.Parsing.AST.Nodes;

namespace MimaSim.Core.Parsing.AST;

public interface INodeVisitor
{
    void Visit(LiteralNode lit);

    void Visit(IdentifierNode id);

    void Visit(CallNode call);
}