﻿using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move;

public class MovRegMemInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.MOV_REG_MEM;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("cu->mem");

        var register = cpu.FetchRegister();

        var address = cpu.Fetch16();

        cpu.Memory.SetValue(address, cpu.GetRegister(register));

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"mov {disassembler.FetchRegister()}, 0x{disassembler.Fetch16():X2}");
    }
}