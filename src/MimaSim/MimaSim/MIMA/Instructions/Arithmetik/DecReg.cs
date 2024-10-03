using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik;

public class DecReg : IInstruction
{
    public OpCodes OpCode => OpCodes.DEC;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("cu->accu");

        var oldValue = cpu.GetRegister(Registers.Accumulator);
        var newValue = oldValue - 1;

        cpu.SetRegister(Registers.Accumulator, (short)newValue);

        return false;
    }
}