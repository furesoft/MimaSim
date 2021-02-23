namespace MimaSim.Core.Parsing
{
    public struct Symbol
    {
        public bool IsRegister { get; set; }
        public bool IsVariable { get; set; }

        public string Name { get; set; }

        public static Symbol CreateRegisterSymbol(string name)
        {
            return new Symbol { IsRegister = true, Name = name };
        }

        public static Symbol CreateVariableSymbol(string name)
        {
            return new Symbol { IsVariable = true, Name = name };
        }
    }
}