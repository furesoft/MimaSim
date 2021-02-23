using MimaSim.Core.Parsing.AST.Nodes;

namespace MimaSim.Core.Parsing.AST
{
    public interface IASTNodePrinter
    {
        string Print(LiteralNode lit);

        string Print(IdentifierNode id);

        string Print(CallNode call);
    }
}