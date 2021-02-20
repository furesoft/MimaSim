using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Jumps
{
    public class JmpInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JMP;

        public string Mnemonic => "jmp";

        public bool Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();

            cpu.SetRegister(VM.Registers.IAR, address);

            return false;
        }
    }
}