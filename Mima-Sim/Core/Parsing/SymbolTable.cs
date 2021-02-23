using System.Collections.Generic;

namespace MimaSim.Core.Parsing
{
    public static class SymbolTable
    {
        private static List<Symbol> _symbols = new();

        public static void AddSymbol(Symbol sym)
        {
            _symbols.Add(sym);
        }

        public static Symbol GetSymbolByName(string name)
        {
            foreach (var sym in _symbols)
            {
                if (sym.Name == name)
                {
                    return sym;
                }
            }

            return new();
        }

        public static bool IsRegister(string name)
        {
            var symbol = GetSymbolByName(name);

            return symbol.IsRegister;
        }

        public static bool IsRegistered(string name)
        {
            var symbol = GetSymbolByName(name);

            return symbol.Name != null;
        }

        public static bool IsVariable(string name)
        {
            var symbol = GetSymbolByName(name);

            return symbol.IsVariable;
        }
    }
}