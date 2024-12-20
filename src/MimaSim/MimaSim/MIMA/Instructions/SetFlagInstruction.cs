﻿using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class SetFlagInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.FLAG;

    public bool Invoke(CPU cpu)
    {
        cpu.ControlUnit.SetFlag((Flags)cpu.Fetch16());

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"flag 0x{disassembler.Fetch16():X2}");
    }
}