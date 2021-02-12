using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class AndRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.AND_REG_REG;
        public string Mnemonic => "and";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            var res = registerValue1 & registerValue2;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}