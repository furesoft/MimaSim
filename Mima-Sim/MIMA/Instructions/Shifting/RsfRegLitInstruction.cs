using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Shifting
{
    public class RsfRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RSF_REG_LIT;
        public string Mnemonic => "rsh";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit8;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch();
            var oldValue = cpu.GetRegister(r1);
            var res = oldValue >> literal;
            cpu.SetRegister(r1, (ushort)res);

            return false;
        }
    }
}