using Silverfly;
using Silverfly.Helpers;

namespace MimaSim.MIMA.Parsing.Parsers.High.Symbols;

public class ParameterSymbol(Token name, TypeName type) : Symbol(name)
{
    public TypeName Type { get; } = type;
}