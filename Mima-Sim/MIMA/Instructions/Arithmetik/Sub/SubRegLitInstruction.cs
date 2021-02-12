using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik.Sub
{
    public class SubRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.SUB_REG_LIT;

        public string Mnemonic => "sub";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);
            var res = literal - registerValue;

            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}