using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions;

public class CallInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.CALL;

    public bool Invoke(CPU cpu)
    {
        var currentAddress = cpu.IAR.GetValueWithoutNotification();
        cpu.Stack.Push(currentAddress);

        var address = cpu.Fetch16();
        cpu.IAR.SetValue(address);

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine($"call 0x{disassembler.Fetch16():X2}");
    }
}