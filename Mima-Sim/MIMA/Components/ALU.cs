namespace MimaSim.MIMA.Components
{
    public class ALU
    {
        private CPU _cpu;

        public Bus OutputBus = new Bus();
        public Bus LeftInputBus = new Bus();
        public Bus RightInputBus = new Bus();

        public Bus CpuBus = new Bus();

        public ALU(CPU cPU)
        {
            this._cpu = cPU;
        }
    }
}