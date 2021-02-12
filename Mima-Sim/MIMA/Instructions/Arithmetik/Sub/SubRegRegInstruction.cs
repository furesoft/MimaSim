using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik.Sub
{
    public class SubRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.SUB_REG_REG;

        public string Mnemonic => "sub";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            cpu.SetRegister(Registers.Accumulator, (ushort)(registerValue1 - registerValue2));

            return false;
        }
    }
}