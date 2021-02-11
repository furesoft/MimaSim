using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.AST.Nodes
{
    public struct CallNode : IAstNode
    {
        public CallNode(string name, object type, List<IAstNode> args)
        {
            Name = name;
            Args = args;
            Type = type;
        }

        public List<IAstNode> Args { get; set; }

        public bool IsEmpty
        {
            get { return !Args.Any(); }
        }

        public string Name { get; set; }
        public object Type { get; set; }

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