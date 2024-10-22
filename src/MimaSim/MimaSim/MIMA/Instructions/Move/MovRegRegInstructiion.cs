using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move;

public class MovRegRegInstructiion : IInstruction, IDisassemblyInstruction
{
    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"mov {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }

    public OpCodes OpCode => OpCodes.MOV_REG_REG;

    public bool Invoke(CPU cpu)
    {
        var registerFrom = cpu.FetchRegister();
        var registerTo = cpu.FetchRegister();
        var value = cpu.GetRegister(registerFrom);

        cpu.SetRegister(registerTo, value);

        return false;
    }
}