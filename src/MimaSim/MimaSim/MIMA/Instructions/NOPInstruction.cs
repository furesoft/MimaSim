using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class NOPInstruction : IInstruction
{
    public OpCodes Instruction => OpCodes.NOP;

    public bool Invoke(CPU cpu)
    {
        return false;
    }
}