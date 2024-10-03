using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik;

public class IncReg : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.INC;

    public bool Invoke(CPU cpu)
    {
        BusRegistry.Activate("cu->accu");

        var oldValue = cpu.GetRegister(Registers.Accumulator);
        var newValue = oldValue + 1;

        cpu.SetRegister(Registers.Accumulator, (short)newValue);

        return false;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"inc {disassembler.FetchRegister()}, {disassembler.FetchRegister()}");
    }
}