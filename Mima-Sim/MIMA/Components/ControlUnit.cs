namespace MimaSim.MIMA.Components
{
    public class ControlUnit
    {
        public Register IAR = new Register("IAR");
        public Register IR = new Register("IR");

        public Bus AkkuBus = new Bus();
    }
}