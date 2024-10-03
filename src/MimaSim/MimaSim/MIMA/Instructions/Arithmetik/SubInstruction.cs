﻿using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik;

public class SubInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.SUB;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("alu->cu");

        var r1 = cpu.GetRegister(Registers.X);
        var r2 = cpu.GetRegister(Registers.Y);

        BusRegistry.Activate("cu->accu");

        cpu.SetRegister(Registers.Accumulator, (short)(r1 - r2));

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"sub");
    }
}