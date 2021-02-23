namespace MimaSim.MIMA.Components
{
    public class Register
    {
        public Bus Bus = new Bus();

        private byte _value;

        public Register(string name, byte value)
            : this(name)
        {
            _value = value;
        }

        public Register(string name)
        {
            RegisterMap.AddRegister(name, this);
        }

        public byte GetValue()
        {
            return _value;
        }

        public void SetValue(byte value)
        {
            _value = value;
            Bus.Send(value);
        }
    }
}