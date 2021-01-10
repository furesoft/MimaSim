using MimaSim.MIMA.Components;
using System.Collections.Generic;

namespace MimaSim.MIMA
{
    public static class RegisterMap
    {
        private static Dictionary<string, Register> _map = new Dictionary<string, Register>();

        public static void AddRegister(string name, Register register)
        {
            if (!_map.ContainsKey(name))
            {
                _map.Add(name, register);
            }
        }

        public static TinyInt GetRegisterValue(string name)
        {
            if (_map.ContainsKey(name))
            {
                return _map[name].GetValue();
            }

            return TinyInt.Invalid;
        }
    }
}