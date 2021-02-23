namespace MimaSim.MIMA.Components
{
    public class Register
    {
        public Bus Bus = new Bus();

        private ushort _value;

        public Register(string name, ushort value)
            : this(name)
        {
            _value = value;
        }

        public Register(string name)
        {
            RegisterMap.AddRegister(name, this);
        }

        public ushort GetValue()
        {
            return _value;
        }

        public void SetValue(ushort value)
        {
            _value = value;
            Bus.Send(value);
        }
    }
}