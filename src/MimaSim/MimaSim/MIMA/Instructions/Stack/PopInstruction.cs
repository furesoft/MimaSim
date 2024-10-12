using System.Text;
using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Stack;

public class PopInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.POP;

    public bool Invoke(CPU cpu)
    {
        cpu.Stack.Pop();
        return true;
    }
    
    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine("pop");
    }
}