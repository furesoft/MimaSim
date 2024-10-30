using System;
using System.Collections.Generic;
using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High.Symbols;

public class SymbolMap
{
    private readonly Dictionary<Token, Symbol> _symbols = new();

    public void AddSymbol(Token name, SymbolType type)
    {
        if (_symbols.ContainsKey(name))
        {
            throw new ArgumentException($"Symbol '{name}' already exists.");
        }

        _symbols[name] = new Symbol(name, type);
    }

    public Symbol GetSymbol(Token name)
    {
        if (_symbols.TryGetValue(name, out var symbol))
        {
            return symbol;
        }

        throw new KeyNotFoundException($"Symbol '{name}' not found.");
    }
}
