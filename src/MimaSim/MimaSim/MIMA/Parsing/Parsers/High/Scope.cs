using System.Collections.Generic;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;
using Symbol = MimaSim.MIMA.Parsing.Parsers.High.Symbols.Symbol;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class Scope(bool isRoot = false)
{
    public Scope? Parent { get; set; }
    public bool IsRoot { get; set; } = isRoot;
    public Dictionary<string, Symbol> Bindings { get; } = [];

    public Scope NewSubScope()
    {
        return new Scope
        {
            Parent = this
        };
    }

    public bool TryGet(string name, out Symbol value)
    {
        return Bindings.TryGetValue(name, out value!);
    }

    public bool TryGet<T>(string name, out T value)
        where T : Symbol
    {
        var result = Bindings.TryGetValue(name, out var tmp);

        value = (T)tmp!;
        return result;
    }

    public void Define(Symbol symbol)
    {
        Bindings[symbol.Name.ToString()] = symbol;
    }

    public Symbol? Get(string name)
    {
        return Bindings.TryGetValue(name, out var value) ? value : Parent?.Get(name)!;
    }

    public Symbol? Get(Token name)
    {
        return Get(name.ToString());
    }
}
