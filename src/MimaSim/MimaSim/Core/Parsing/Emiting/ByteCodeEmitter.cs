using System.Collections.Generic;
using MimaSim.MIMA;

namespace MimaSim.Core.Parsing.Emiting;

public class ByteCodeEmitter
{
    private readonly ByteArrayBuilder _builder = new();
    private readonly Dictionary<string, short> _labels = new();
    private readonly List<short> _labelReferences = new();
    public int Position => _builder.Length;

    public void Append(byte[] raw)
    {
        _builder.Append(raw, false);
    }

    public Label DefineLabel()
    {
        return new Label(_labels.Count);
    }

    public void EmitInstruction(OpCodes opcode)
    {
        EmitOpcode(opcode);
    }

    public void EmitInstruction(OpCodes opcode, short value)
    {
        EmitOpcode(opcode);
        EmitLiteral(value);
    }

    public void EmitInstruction(OpCodes opcode, Registers reg1, Registers reg2)
    {
        EmitOpcode(opcode);
        EmitRegister(reg1);
        EmitRegister(reg2);
    }

    public void EmitInstruction(OpCodes opcode, Registers reg1, short address)
    {
        EmitOpcode(opcode);
        EmitRegister(reg1);
        EmitLiteral(address);
    }

    public void EmitLiteral(short value)
    {
        _builder.Append(value);
    }

    public void EmitOpcode(OpCodes op)
    {
        _builder.Append((byte)op);
    }

    public void EmitRegister(Registers reg)
    {
        _builder.Append((byte)reg);
    }

    public void AddLabelReference()
    {
        _labelReferences.Add((short)_builder.Length);
        _builder.Append((short)0);
    }

    private void ReplaceLabelReferences()
    {
        foreach (var kvp in _labels)
        {
            int index = 0;
            while (index < _labelReferences.Count)
            {
                if (_labelReferences[index] == kvp.Value)
                {
                    _builder.ReplaceAt(index, kvp.Value);
                }
                index++;
            }
        }

        _labelReferences.Clear();
    }

    public byte[] ToArray()
    {
        ReplaceLabelReferences(); // Ensure labels are replaced before converting to array
        return _builder.ToArray();
    }

    public void CreateLabel(string name)
    {
        _labels.Add(name, (byte)_builder.Length);
    }
}