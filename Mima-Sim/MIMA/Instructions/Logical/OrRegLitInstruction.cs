using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class OrRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.OR_REG_LIT;
        public string Mnemonic => "or";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);

            var res = registerValue | literal;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}