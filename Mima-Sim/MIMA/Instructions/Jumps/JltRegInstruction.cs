using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Jumps
{
    public class JltRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JLT_REG;
        public string Mnemonic => "jlt";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var value = cpu.GetRegister(r1);
            var address = cpu.Fetch16();

            if (value < cpu.GetRegister(Registers.Accumulator))
            {
                cpu.SetRegister(Registers.IAR, address);
            }

            return false;
        }
    }
}