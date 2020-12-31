namespace MimaSim.MIMA.Components
{
    public class Register
    {
        public Bus Bus = new Bus();

        private TinyInt _value;

        public Register(TinyInt value)
        {
            _value = value;
        }

        public Register()
        {
        }

        public void SetValue(TinyInt value)
        {
            _value = value;
        }

        public TinyInt GetValue()
        {
            return _value;
        }
    }
}