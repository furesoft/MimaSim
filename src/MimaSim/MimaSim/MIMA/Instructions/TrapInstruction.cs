using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class TrapInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.TRAP;

    public bool Invoke(CPU cpu)
    {
        cpu.ControlUnit.SetFlag(Flags.Trap);
        CPU.Instance.Clock.Stop();

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"trap");
    }
}