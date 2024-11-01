using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Jumps;

public class JmpInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.JMP;

    public bool Invoke(CPU cpu)
    {
        var address = cpu.Fetch16();

        cpu.SetRegister(Registers.IAR, address);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"jmp 0x{disassembler.Fetch16():X2}");
    }
}