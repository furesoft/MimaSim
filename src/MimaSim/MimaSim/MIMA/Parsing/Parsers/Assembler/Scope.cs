using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class Scope(bool isRoot = false)
{
    public Scope? Parent { get; set; }
    public bool IsRoot { get; set; } = isRoot;
    public Dictionary<string, object> Bindings { get; set; } = [];

    public static readonly Scope Root = new(true);

    public Scope NewSubScope()
    {
        return new Scope
        {
            Parent = this
        };
    }

    public void Define(string name, object value)
    {
        Bindings[name] = value;
    }

    public object? Get(string name)
    {
        if (Bindings.TryGetValue(name, out var value))
        {
            return value;
        }

        return Parent?.Get(name)!;
    }
}