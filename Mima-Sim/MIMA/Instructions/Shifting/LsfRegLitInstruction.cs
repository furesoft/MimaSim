using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Shifting
{
    public class LsfRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.LSF_REG_LIT;
        public string Mnemonic => "lsh";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit8;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch();
            var oldValue = cpu.GetRegister(r1);
            var res = oldValue << literal;
            cpu.SetRegister(r1, (ushort)res);

            return false;
        }
    }
}