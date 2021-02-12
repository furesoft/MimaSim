using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Shifting
{
    public class LsfRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.LSF_REG_REG;
        public string Mnemonic => "lsh";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var oldValue = cpu.GetRegister(r1);
            var shiftBy = cpu.GetRegister(r2);
            var res = oldValue << shiftBy;
            cpu.SetRegister(r1, (ushort)res);

            return false;
        }
    }
}