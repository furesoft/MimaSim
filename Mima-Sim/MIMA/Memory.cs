namespace MimaSim.MIMA
{
    public class Memory
    {
        private TinyInt[] _values;

        public Memory(int length)
        {
            _values = new TinyInt[length];
        }

        public TinyInt GetValue(TinyInt address)
        {
            if (address.Value > 0 && address.Value < _values.Length)
            {
                return _values[address.Value];
            }

            return TinyInt.Invalid;
        }

        public void SetValue(TinyInt address, TinyInt value)
        {
            if (address.Value > 0 && address.Value < _values.Length)
            {
                _values[address.Value] = value;
            }
        }
    }
}