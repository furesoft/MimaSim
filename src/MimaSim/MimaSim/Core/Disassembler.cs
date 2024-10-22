using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MimaSim.MIMA;

namespace MimaSim.Core;

public class Disassembler(byte[] program)
{
    private static readonly Dictionary<OpCodes, IDisassemblyInstruction> Instructions = new();

    static Disassembler()
    {
        var types = Assembly.GetCallingAssembly().GetTypes()
            .Where(_ => _.GetInterfaces().Contains(typeof(IDisassemblyInstruction)));
        foreach (var t in types)
        {
            var instance = (IDisassemblyInstruction)Activator.CreateInstance(t)!;

            Instructions.Add(instance.OpCode, instance);
        }
    }

    private int Position { get; set; }
    private byte[] Program { get; } = program;

    public byte Fetch()
    {
        return Program[Position++];
    }

    public short Fetch16()
    {
        var first = Fetch();
        var second = Fetch();

        return BitConverter.ToInt16([first, second], 0);
    }

    public Registers FetchRegister()
    {
        return (Registers)Fetch();
    }

    public string Disassemble()
    {
        var builder = new StringBuilder();

        while (Position < Program.Length)
        {
            var op = Fetch();
            var instruction = Instructions[(OpCodes)op];

            instruction.Dissassemble(builder, this);
        }

        return builder.ToString();
    }
}