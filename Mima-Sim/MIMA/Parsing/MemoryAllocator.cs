using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing
{
    public static class MemoryAllocator
    {
        private static Dictionary<string, ushort> _variableAllocations = new();

        public static ushort Allocate(string name)
        {
            if (_variableAllocations.ContainsKey(name))
            {
                return _variableAllocations[name];
            }
            else
            {
                var address = (ushort)(_variableAllocations.Count + 1);
                _variableAllocations.Add(name, address);

                return address;
            }
        }
    }
}