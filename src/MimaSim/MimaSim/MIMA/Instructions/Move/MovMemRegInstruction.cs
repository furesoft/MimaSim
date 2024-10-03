using System;
using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move;

public class MovMemRegInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.MOV_MEM_REG;

    public bool Invoke(CPU cpu)
    {
        var address = cpu.Fetch16();
        BusRegistry.Activate("cu->mem");

        var value = cpu.Memory.GetValue(address);
        var register = cpu.FetchRegister();

        cpu.SetRegister(register, value);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"mov {disassembler.Fetch16()}, {disassembler.FetchRegister()}");
    }
}