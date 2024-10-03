using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Compare;

public class NotCompareInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.CMPN;

    public bool Invoke(CPU cpu)
    {
        var accu = cpu.GetRegister(Registers.Accumulator);

        cpu.SetRegister(Registers.Accumulator, accu == 1 ? (short)0 : (short)1);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"cmpn {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }
}