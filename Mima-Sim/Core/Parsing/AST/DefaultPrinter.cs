/// Copyright by Chris Anders (filmee24, Furesoft)
/// Copyright by Chris Anders (filmee24, Furesoft)
using MimaSim.Core.Parsing.AST.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MimaSim.Core.Parsing.AST
{
    public class DefaultPrinter : IPrinter
    {
        public static string SeperateArgs(IEnumerable<IAstNode> a)
        {
            var args = a.Select(_ => _.ToString());
            return string.Join(',', args);
        }

        public string Print(LiteralNode lit)
        {
            return lit.Value.ToString();
        }

        public string Print(IdentifierNode id)
        {
            return id.Name;
        }

        public string Print(CallNode call)
        {
            var sb = new StringBuilder();

            sb.Append(call.Type);
            sb.Append('(');

            sb.Append(SeperateArgs(call.Args));

            sb.Append(')');

            return sb.ToString();
        }
    }
}