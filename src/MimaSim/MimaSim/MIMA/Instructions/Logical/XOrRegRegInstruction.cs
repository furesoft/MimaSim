﻿using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Logical;

public class XOrRegRegInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.XOR;

    public bool Invoke(CPU cpu)
    {
        var registerValue1 = cpu.GetRegister(Registers.X);
        var registerValue2 = cpu.GetRegister(Registers.Y);

        var res = registerValue1 ^ registerValue2;
        cpu.SetRegister(Registers.Accumulator, (byte)res);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"xor {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }
}