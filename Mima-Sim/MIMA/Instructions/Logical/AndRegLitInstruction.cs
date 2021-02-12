using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class AndRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.AND_REG_LIT;
        public string Mnemonic => "and";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);

            var res = registerValue & literal;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}