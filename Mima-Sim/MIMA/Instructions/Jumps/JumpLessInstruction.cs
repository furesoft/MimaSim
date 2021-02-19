using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Jumps
{
    public class JumpLessInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JLT;
        public string Mnemonic => "jlt";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value < cpu.GetRegister(Registers.Accumulator))
            {
                cpu.SetRegister(Registers.IAR, address);
            }

            return false;
        }
    }
}