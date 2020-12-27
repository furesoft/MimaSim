namespace MimaSim.MIMA
{
    public struct Register
    {
        private TinyInt _value;

        public Register(TinyInt value)
        {
            _value = value;
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