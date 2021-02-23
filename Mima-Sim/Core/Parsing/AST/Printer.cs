using MimaSim.Core.AST.Nodes;
using System;

namespace MimaSim.Core.AST
{
    public static class Printer
    {
        public static volatile IPrinter Default = new DefaultPrinter();

        public static string Print(IAstNode value)
        {
            if (value is LiteralNode lit)
            {
                return Default.Print(lit);
            }
            else if (value is IdentifierNode id)
            {
                return Default.Print(id);
            }
            else if (value is CallNode call)
            {
                return Default.Print(call);
            }

            return string.Empty;
        }

        public static IDisposable Scoped(IPrinter printer)
        {
            return new ScopedPrinter(printer);
        }
    }
}