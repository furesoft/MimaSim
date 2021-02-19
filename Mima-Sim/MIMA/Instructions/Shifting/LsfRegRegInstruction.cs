using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Shifting
{
    public class LsfRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.LSHIFT;
        public string Mnemonic => "lsh";

        public bool Invoke(CPU cpu)
        {
            var oldValue = cpu.GetRegister(Registers.X);
            var shiftBy = cpu.GetRegister(Registers.Y);
            var res = oldValue << shiftBy;

            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}