using MimaSim.MIMA.Parsing.Parsers.High.Symbols;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class NameMangler
{
    public static string Mangle(Symbol symbol)
    {
        return $"__{symbol.Name}__:";
    }
}