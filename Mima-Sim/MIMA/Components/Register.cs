namespace MimaSim.MIMA.Components
{
    public class Register
    {
        public Bus Bus = new Bus();

        private short _value;

        public Register(string name, short value)
            : this(name)
        {
            _value = value;
        }

        public Register(string name)
        {
            RegisterMap.AddRegister(name, this);
        }

        public short GetValue()
        {
            Bus.Send(_value);

            return _value;
        }

        public void SetValue(short value)
        {
            _value = value;
            Bus.Send(value);
        }
    }
}