namespace MimaSim.MIMA.Components
{
    public class ControlUnit
    {
        public Bus AccuBus = new Bus();
        public Register IAR = new Register("IAR");
        public Register IR = new Register("IR");
    }
}