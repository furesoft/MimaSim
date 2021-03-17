namespace MimaSim.MIMA.Components
{
    public class ControlUnit
    {
        public Bus AccuBus = new();
        public Register IAR = new("IAR");
    }
}