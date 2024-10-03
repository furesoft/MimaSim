using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class NOPInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.NOP;
    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine("nop");
    }

    public bool Invoke(CPU cpu)
    {
        return false;
    }
}