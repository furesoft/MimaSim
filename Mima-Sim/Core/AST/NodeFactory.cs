using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing;
using System.Collections.Generic;

namespace MimaSim.Core.AST
{
    public static class NodeFactory
    {
        public static IAstNode Call(AstCallNodeType type, params IAstNode[] args)
        {
            return new CallNode(type, new List<IAstNode>(args));
        }

        public static IAstNode Id(string name)
        {
            return new IdentifierNode(name);
        }

        public static IAstNode Literal(object value)
        {
            return new LiteralNode(value);
        }
    }
}