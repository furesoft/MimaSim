using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class SysCallInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.SYSCALL;

    public bool Invoke(CPU cpu)
    {
        cpu.ControlUnit.InvokeSysCall((SysCall)cpu.Fetch16());

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"syscall 0x{disassembler.Fetch16():X2}");
    }
}