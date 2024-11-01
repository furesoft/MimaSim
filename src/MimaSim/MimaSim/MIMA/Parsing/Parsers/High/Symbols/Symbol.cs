using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High.Symbols;

public class Symbol(Token name)
{
    public Token Name { get; } = name;

}