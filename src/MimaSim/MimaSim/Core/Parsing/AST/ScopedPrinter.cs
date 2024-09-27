using System;

namespace MimaSim.Core.Parsing.AST;

public class ScopedPrinter : IDisposable
{
    public ScopedPrinter(IPrinter printer)
    {
        Printer.Default = printer;
    }

    public void Dispose()
    {
        Printer.Default = new DefaultPrinter();
    }
}