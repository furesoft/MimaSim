using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class HasFlagInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.HASFLAG;

    public bool Invoke(CPU cpu)
    {
        var current = cpu.ControlUnit.FLAG.GetValueWithoutNotification();
        int bitIndex = cpu.Fetch16();

        var isBitSet = (current & (1 << bitIndex)) != 0;
        cpu.Accumulator.SetValue(isBitSet ? (short)1 : (short)0);

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"hasflag 0x{disassembler.Fetch16():X2}");
    }
}