namespace MimaSim.MIMA
{
    public class TinyInt
    {
        public static TinyInt MaxValue = 8388608;

        public TinyInt(int value)
        {
            Value = value & 0xFFFFFF;
            HasValue = true;
        }

        public static TinyInt Invalid { get; }
        public bool HasValue { get; set; }

        public int Value { get; }

        public static implicit operator TinyInt(int value)
        {
            return new TinyInt(value);
        }

        public override string ToString()
        {
            if (HasValue)
            {
                return Value.ToString();
            }

            return "null";
        }
    }
}