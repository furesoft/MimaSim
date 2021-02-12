using MimaSim.MIMA.Components;

namespace MimaSim.Core
{
    public interface IInstruction
    {
        OpCodes Instruction { get; }
        string Mnemonic { get; }
        InstructionTypeSizes Size { get; }

        bool Invoke(CPU cpu);
    }
}