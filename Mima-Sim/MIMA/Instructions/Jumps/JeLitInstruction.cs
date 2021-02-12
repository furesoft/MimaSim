using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Jumps
{
    public class JeLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JEQ_LIT;
        public string Mnemonic => "je";
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value == cpu.GetRegister(Registers.Accumulator))
            {
                cpu.SetRegister(Registers.IAR, address);
            }

            return false;
        }
    }
}