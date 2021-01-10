using System;

namespace MimaSim.MIMA.Components
{
    public class Memory
    {
        public Register AR = new Register("AR");
        public Register DR = new Register("DR");

        private TinyInt[] _values;

        public Memory(int length)
        {
            _values = new TinyInt[length];
        }

        public Memory(TinyInt[] parent)
        {
            _values = parent;
        }

        public void Expand(int length)
        {
            TinyInt[] buffer = new TinyInt[length];

            Array.Copy(_values, buffer, _values.Length);
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