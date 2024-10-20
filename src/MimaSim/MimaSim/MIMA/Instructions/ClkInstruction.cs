using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class ClkInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.CLK;

    public bool Invoke(CPU cpu)
    {
        cpu.Clock.SetFrequency(cpu.Fetch16());

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"clk 0x{disassembler.Fetch16():X2}");
    }
}