using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Jumps;

public class JmpInstruction : IInstruction
{
    public OpCodes Instruction => OpCodes.JMP;

    public bool Invoke(CPU cpu)
    {
        var address = cpu.Fetch();

        cpu.SetRegister(Registers.IAR, address);

        return false;
    }
}