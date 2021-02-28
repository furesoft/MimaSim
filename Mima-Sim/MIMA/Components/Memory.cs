using MimaSim.Messages;
using ReactiveUI;
using System;

namespace MimaSim.MIMA.Components
{
    public class Memory
    {
        private readonly short[] _values;

        public Memory(int length)
        {
            _values = new short[length];
        }

        public Memory(short[] parent)
        {
            _values = parent;
        }

        public void Expand(int length)
        {
            byte[] buffer = new byte[length];

            Array.Copy(_values, buffer, _values.Length);
        }

        public short GetValue(short address)
        {
            if (address > 0 && address < _values.Length)
            {
                CPU.Instance.SetRegister(Registers.SAR, address);
                CPU.Instance.SetRegister(Registers.SDR, _values[address]);

                return _values[address];
            }

            return 0;
        }

        public void SetValue(short address, short value)
        {
            if (address >= 0 && address < _values.Length)
            {
                _values[address] = value;

                CPU.Instance.SetRegister(Registers.SAR, address);
                CPU.Instance.SetRegister(Registers.SDR, _values[address]);

                MessageBus.Current.SendMessage(new MemoryCellChangedMessage { Address = address, Value = value });
            }
        }
    }
}