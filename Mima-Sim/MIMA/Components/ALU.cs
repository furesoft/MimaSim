namespace MimaSim.MIMA.Components
{
    public class ALU
    {
        public Bus CpuBus = new Bus();
        public Bus LeftInputBus = new Bus();
        public Bus OutputBus = new Bus();
        public Bus RightInputBus = new Bus();
        private CPU _cpu;

        public ALU(CPU cPU)
        {
            this._cpu = cPU;
        }
    }
}