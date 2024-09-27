using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing;

public static class MemoryAllocator
{
    private static readonly Dictionary<string, byte> _variableAllocations = new();

    public static byte Allocate(string name)
    {
        if (_variableAllocations.ContainsKey(name))
        {
            return _variableAllocations[name];
        }
        else
        {
            var address = (byte)(_variableAllocations.Count + 1);
            _variableAllocations.Add(name, address);

            return address;
        }
    }
}