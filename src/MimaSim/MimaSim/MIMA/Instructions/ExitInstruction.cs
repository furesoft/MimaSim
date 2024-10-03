using System;
using System.Text;
using MimaSim.Core;
using MimaSim.Messages;
using MimaSim.MIMA.Components;
using ReactiveUI;

namespace MimaSim.MIMA.Instructions;

public class ExitInstruction : IInstruction, IDisassemblyInstruction
{
    public OpCodes OpCode => OpCodes.EXIT;

    public bool Invoke(CPU cpu)
    {
        cpu.Clock.Stop();
        MessageBus.Current.SendMessage(new StopMessage());

        return true;
    }

    public void Dissassemble(StringBuilder builder, Disassembler disassembler)
    {
        builder.AppendLine("exit");
    }
}