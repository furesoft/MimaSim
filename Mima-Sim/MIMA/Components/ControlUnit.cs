using System;

namespace MimaSim.MIMA
{
    public class ControlUnit
    {
        public Register IAR = new Register();
        public Register IR = new Register();

        public Bus AkkuBus = new Bus();
    }
}