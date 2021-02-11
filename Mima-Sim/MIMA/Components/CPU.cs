using System;

namespace MimaSim.MIMA.Components
{
    public class CPU
    {
        public static CPU Instance = new CPU();

        public Register Accumulator = new Register("Accumulator", 42);
        public ALU ALU;
        public Clock Clock = new Clock(1024);
        public ControlUnit ControlUnit = new ControlUnit();
        public Bus DataBus = new Bus();

        public Memory Memory = new Memory((int)Math.Pow(2, 24));
        public Register One = new Register("One", 1);
        public Register SAR = new Register("SAR");
        public Register SDR = new Register("SDR");
        public Register X = new Register("X");
        public Register Y = new Register("Y");
        public Register Z = new Register("Z");

        public CPU()
        {
            ALU = new ALU(this);
        }

        public byte[] Program { get; internal set; }

        public void Fetch()
        {
            var address = ControlUnit.IAR.GetValue();
        }

        //Hack for Init all Fields
        public void Init()
        {
        }

        public void Run()
        {
            while (!Step())
            {
            }
        }

        public bool Step()
        {
            //fetch mnemnonic

            return false;
        }
    }
}