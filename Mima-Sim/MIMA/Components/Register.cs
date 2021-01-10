namespace MimaSim.MIMA.Components
{
    public class Register
    {
        public Bus Bus = new Bus();

        private TinyInt _value;

        public Register(string name, TinyInt value)
            : this(name)
        {
            _value = value;
        }

        public Register(string name)
        {
            RegisterMap.AddRegister(name, this);
        }

        public void SetValue(TinyInt value)
        {
            _value = value;
            Bus.Send(value);
        }

        public TinyInt GetValue()
        {
            return _value;
        }
    }
}