using MimaSim.Messages;
using ReactiveUI;
using System;

namespace MimaSim.MIMA.Components
{
    public class Memory
    {
        private ushort[] _values;

        public Memory(int length)
        {
            _values = new ushort[length];
        }

        public Memory(ushort[] parent)
        {
            _values = parent;
        }

        public void Expand(int length)
        {
            ushort[] buffer = new ushort[length];

            Array.Copy(_values, buffer, _values.Length);
        }

        public ushort GetValue(ushort address)
        {
            if (address > 0 && address < _values.Length)
            {
                return _values[address];
            }

            return 0;
        }

        public void SetValue(ushort address, ushort value)
        {
            if (address > 0 && address < _values.Length)
            {
                _values[address] = value;

                MessageBus.Current.SendMessage(new MemoryCellChangedMessage { Address = address, Value = value });
            }
        }
    }
}