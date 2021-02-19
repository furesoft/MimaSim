using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions
{
    public class LoadInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.LOAD;
        public string Mnemonic => "load";

        public bool Invoke(CPU cpu)
        {
            cpu.SetRegister(VM.Registers.Accumulator, cpu.Fetch16());

            return true;
        }
    }
}