using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High.Symbols;

public class Symbol(Token name, SymbolType type)
{
    public Token Name { get; } = name;
    public SymbolType Type { get; } = type;
}