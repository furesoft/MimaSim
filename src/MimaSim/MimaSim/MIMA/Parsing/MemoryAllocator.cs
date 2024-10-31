using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing;

public class MemoryAllocator
{
    private readonly Dictionary<string, (int Address, Type DataType)> _variables;
    private int _nextAddress;

    public StaticMemoryAllocator()
    {
        _variables = new Dictionary<string, (int, Type)>();
        _nextAddress = 0;
    }

    public void AllocateVariable(string name, Type dataType)
    {
        if (_variables.ContainsKey(name))
        {
            throw new ArgumentException($"Variable '{name}' ist bereits allokiert.");
        }

        _variables[name] = (_nextAddress++, dataType);
    }

    public void AllocateStruct(string structName, Dictionary<string, Type> members)
    {
        if (_variables.ContainsKey(structName))
        {
            throw new ArgumentException($"Struct '{structName}' ist bereits allokiert.");
        }

        int structSize = CalculateStructSize(members);
        _variables[structName] = (_nextAddress, typeof(StructWrapper));
        _nextAddress += structSize;
    }

    private int CalculateStructSize(Dictionary<string, Type> members)
    {
        int size = 0;
        foreach (var member in members.Values)
        {
            size += System.Runtime.InteropServices.Marshal.SizeOf(member);
        }
        return size;
    }

    public (int Address, Type DataType) GetVariableInfo(string name)
    {
        if (!_variables.TryGetValue(name, out var info))
        {
            throw new KeyNotFoundException($"Variable '{name}' wurde nicht gefunden.");
        }
        return info;
    }

    public class StructWrapper
    {
        public int Address { get; set; }
        public Dictionary<string, Type> Members { get; set; }
    }
}