using MimaSim.MIMA.Components;
using System.Collections.Generic;

namespace MimaSim.MIMA;

public static class RegisterMap
{
    private static readonly Dictionary<string, Register> _map = new();

    public static void AddRegister(string name, Register register)
    {
        if (!_map.ContainsKey(name))
        {
            _map.Add(name, register);
        }
    }

    public static Register GetRegister(string name)
    {
        if (_map.ContainsKey(name))
        {
            return _map[name];
        }

        return null;
    }

    public static void InitAllRegisters()
    {
        foreach (var reg in _map)
        {
            reg.Value.SetValue(0);
        }
    }
}