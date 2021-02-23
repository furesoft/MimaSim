using MimaSim.Core.AST.Nodes;

namespace MimaSim.Core.AST
{
    public interface IASTNodePrinter
    {
        string Print(LiteralNode lit);

        string Print(IdentifierNode id);

        string Print(CallNode call);
    }
}