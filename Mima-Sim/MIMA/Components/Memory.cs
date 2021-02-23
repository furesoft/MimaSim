using MimaSim.Messages;
using ReactiveUI;
using System;

namespace MimaSim.MIMA.Components
{
    public class Memory
    {
        private readonly byte[] _values;

        public Memory(int length)
        {
            _values = new byte[length];
        }

        public Memory(byte[] parent)
        {
            _values = parent;
        }

        public void Expand(int length)
        {
            byte[] buffer = new byte[length];

            Array.Copy(_values, buffer, _values.Length);
        }

        public byte GetValue(byte address)
        {
            if (address > 0 && address < _values.Length)
            {
                CPU.Instance.SetRegister(Registers.SAR, address);
                CPU.Instance.SetRegister(Registers.SDR, _values[address]);

                return _values[address];
            }

            return 0;
        }

        public void SetValue(byte address, byte value)
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