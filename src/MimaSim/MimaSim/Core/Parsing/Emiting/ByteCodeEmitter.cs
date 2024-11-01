using System;
using System.Collections.Generic;
using MimaSim.MIMA;

namespace MimaSim.Core.Parsing.Emiting;

public class ByteCodeEmitter
{
    private readonly ByteArrayBuilder _builder = new();
    private readonly Dictionary<string, short> _labels = new();
    private readonly List<(int position, string labelName)> _unresolvedLabels = new();

    public int Position => _builder.Length;

    public void Append(byte[] raw)
    {
        _builder.Append(raw, false);
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

    public void AddLabelReference(string labelName)
    {
        // Store unresolved label reference with current position
        _unresolvedLabels.Add((Position, labelName));
        // Append a placeholder for now
        _builder.Append((short)0); // Placeholder for unresolved labels
    }

    public void CreateLabel(string name)
    {
        // Store the current position as the address of the label
        if (!_labels.ContainsKey(name))
        {
            _labels[name] = (short)Position;
        }
        else
        {
            throw new InvalidOperationException($"Label '{name}' is already defined.");
        }
    }

    public void ResolveLabels()
    {
        foreach (var (position, labelName) in _unresolvedLabels)
        {
            if (_labels.TryGetValue(labelName, out var address))
            {
                // Replace placeholder with actual address
                _builder.ReplaceAt(position, address);
            }
            else
            {
                throw new InvalidOperationException($"Label '{labelName}' was never defined.");
            }
        }

        // Clear unresolved references after processing
        _unresolvedLabels.Clear();
    }

    public byte[] ToArray()
    {
        ResolveLabels(); // Ensure all labels are resolved before converting to array
        return _builder.ToArray();
    }
}