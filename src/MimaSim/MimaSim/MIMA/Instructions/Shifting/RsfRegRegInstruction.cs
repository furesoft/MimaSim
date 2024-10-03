using System;
using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Shifting;

public class RsfRegRegInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.RSHIFT;

    public bool Invoke(CPU cpu)
    {
        var oldValue = cpu.GetRegister(Registers.X);
        var shiftBy = cpu.GetRegister(Registers.Y);
        var res = oldValue >> shiftBy;

        cpu.SetRegister(Registers.Accumulator, (byte)res);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"rshift {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }
}