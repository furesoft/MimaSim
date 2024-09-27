using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class LoadInstruction : IInstruction
{
    public OpCodes Instruction => OpCodes.LOAD;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("cu->accu");
        cpu.SetRegister(Registers.Accumulator, cpu.Fetch16());

        return true;
    }
}