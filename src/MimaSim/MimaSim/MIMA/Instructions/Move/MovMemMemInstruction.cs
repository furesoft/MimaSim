using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move;

public class MovMemMemInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.MOV_MEM_MEM;

    public bool Invoke(CPU cpu)
    {
        var fromAddress = cpu.Fetch16();

        BusRegistry.Activate("cu->mem");

        var value = cpu.Memory.GetValue(fromAddress);

        var toAddress = cpu.Fetch16();

        cpu.Memory.SetValue(toAddress, value);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"mov {disassembler.Fetch16()}, {disassembler.Fetch16()}");
    }
}