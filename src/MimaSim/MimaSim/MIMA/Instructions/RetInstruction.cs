using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class RetInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.RET;

    public bool Invoke(CPU cpu)
    {      
        cpu.Stack.Pop();

        var returnAddress = cpu.Accumulator.GetValueWithoutNotification();
        cpu.IAR.SetValue(returnAddress);

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine("ret");
    }
}