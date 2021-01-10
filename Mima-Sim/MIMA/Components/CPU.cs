using System;

namespace MimaSim.MIMA.Components
{
    public class CPU
    {
        public static CPU Instance = new CPU();

        public Bus DataBus = new Bus();

        public Register SAR = new Register("SAR");
        public Register SDR = new Register("SDR");

        public Register Accumulator = new Register("Accumulator", 42);
        public Register One = new Register("One", 1);

        public Register X = new Register("X");
        public Register Y = new Register("Y");
        public Register Z = new Register("Z");

        public Memory Memory = new Memory((int)Math.Pow(2, 24));

        public Clock Clock = new Clock(1024);

        public ControlUnit ControlUnit = new ControlUnit();

        public ALU ALU;

        public byte[] Program { get; internal set; }

        public CPU()
        {
            ALU = new ALU(this);
        }

        public bool Step()
        {
            return false;
        }

        public void Run()
        {
            while (!Step())
            {
            }
        }

        //Hack for Init all Fields
        public void Init()
        {
        }
    }
}