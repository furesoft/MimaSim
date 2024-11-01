using Silverfly;
using Silverfly.Helpers;

namespace MimaSim.MIMA.Parsing.Parsers.High.Symbols;

public class FunctionSymbol(Token name, TypeName type) : Symbol(name)
{
    public TypeName Type { get; } = type;
}