using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Stack;

public class PushInstruction : IInstruction, IDisassemblyInstruction
{
    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine("push");
    }

    public OpCodes OpCode => OpCodes.PUSH;

    public bool Invoke(CPU cpu)
    {
        cpu.Stack.Push();
        return true;
    }
}