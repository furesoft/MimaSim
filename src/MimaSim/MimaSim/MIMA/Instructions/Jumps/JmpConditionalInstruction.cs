using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Jumps;

public class JmpConditionalInstruction : IInstruction
{
    public OpCodes OpCode => OpCodes.JMPC;

    public bool Invoke(CPU cpu)
    {
        var address = cpu.Fetch16();

        var cond = cpu.GetRegister(Registers.Accumulator) != 0;

        if (cond)
        {
            cpu.SetRegister(Registers.IAR, address);
        }

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"jmpc {disassembler.Fetch()}");
    }
}