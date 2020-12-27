namespace MimaSim.MIMA
{
    public class ALU
    {
        private CPU _cpu;

        public Register X = new Register();
        public Register Y = new Register();
        public Register Z = new Register();

        public ALU(CPU cPU)
        {
            this._cpu = cPU;
        }
    }
}