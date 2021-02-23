using MimaSim.Core.Parsing.AST.Nodes;

namespace MimaSim.Core.Parsing.AST
{
    public static class NodeExtensions
    {
        public static IAstNode AddArg(this CallNode node, IAstNode arg)
        {
            node.Args.Add(arg);
            return node;
        }
    }
}