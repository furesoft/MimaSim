using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class LoadInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.LOAD;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("cu->accu");
        cpu.SetRegister(Registers.Accumulator, cpu.Fetch16());

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"load 0x{disassembler.Fetch16():X2}");
    }
}