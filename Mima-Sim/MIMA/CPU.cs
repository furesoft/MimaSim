using System;

namespace MimaSim.MIMA
{
    public class CPU
    {
        public Bus DataBus = new Bus();

        public Register SAR = new Register();
        public Register SDR = new Register();

        public Register Accumulator = new Register();
        public Register One = new Register(1);

        public Register X = new Register();
        public Register Y = new Register();
        public Register Z = new Register();

        public Memory Memory = new Memory((int)Math.Pow(2, 24));

        public Clock Clock = new Clock(1024);

        public ControlUnit ControlUnit = new ControlUnit();

        public ALU ALU;

        public CPU()
        {
            ALU = new ALU(this);
        }
    }
}