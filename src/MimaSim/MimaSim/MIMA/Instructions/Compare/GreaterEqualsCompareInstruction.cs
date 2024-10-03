using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Compare;

public class GreaterEqualsCompareInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.CMPGE;

    public bool Invoke(CPU cpu)
    {
        var left = cpu.GetRegister(Registers.X);
        var right = cpu.GetRegister(Registers.Y);

        var value = left >= right;

        cpu.SetRegister(Registers.Accumulator, (short)(value ? 1 : 0));

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"cmpge {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }
}