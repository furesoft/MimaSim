using MimaSim.MIMA.Parsing;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Parsing.AST.Nodes
{
    public struct CallNode : IAstNode
    {
        public CallNode(AstCallNodeType type, List<IAstNode> args)
        {
            Args = args;
            Type = type;
        }

        public List<IAstNode> Args { get; set; }

        public bool IsEmpty
        {
            get { return !Args.Any(); }
        }

        public AstCallNodeType Type { get; set; }

        public override string ToString()
        {
            return Printer.Default.Print(this);
        }

        public void Visit(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}