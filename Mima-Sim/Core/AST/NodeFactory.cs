using MimaSim.Core.AST.Nodes;
using System.Collections.Generic;

namespace MimaSim.Core.AST
{
    public static class NodeFactory
    {
        public static IAstNode Call(string name, object type, params IAstNode[] args)
        {
            return new CallNode(name, type, new List<IAstNode>(args));
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