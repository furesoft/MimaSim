using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PreparationVisitor : NodeVisitor
{
    public Symbols.SymbolMap SymbolMap =  new();
    public PreparationVisitor()
    {

    }
}