﻿using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Logical;

public class NotInstruction : IInstruction, IDisassemblyInstruction
{
    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"not {disassembler.FetchRegister()}");
    }

    public OpCodes OpCode => OpCodes.NOT;

    public bool Invoke(CPU cpu)
    {
        var registerValue = cpu.GetRegister(Registers.X);

        var res = ~registerValue & 0xffff;
        cpu.SetRegister(Registers.Accumulator, (byte)res);

        return false;
    }
}