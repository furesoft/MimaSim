using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovLitMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_LIT_MEM;
        public string Mnemonic => "move";
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            cpu.Memory.SetValue(address, value);

            return false;
        }
    }
}