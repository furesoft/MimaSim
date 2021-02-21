using MimaSim.MIMA;
using System;
using System.Collections.Generic;

namespace MimaSim.Core.Emiting
{
    public class ByteCodeEmitter
    {
        private readonly ByteArrayBuilder _builder = new ByteArrayBuilder();

        private readonly Dictionary<string, ushort> _labels = new Dictionary<string, ushort>();
        public int Position => _builder.Length;

        public ushort CreateLabel(string name)
        {
            _labels.Add(name, (ushort)_builder.Length);

            return (ushort)_builder.Length;
        }

        public void EmitInstruction(OpCodes opcode)
        {
            EmitOpcode(opcode);
        }

        public void EmitInstruction(OpCodes opcode, ushort value)
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

        public void EmitLiteral(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);

            _builder.Append(bytes, addLength: false);
        }

        public void EmitOpcode(OpCodes op)
        {
            _builder.Append((byte)op);
        }

        public void EmitRegister(Registers reg)
        {
            _builder.Append((byte)reg);
        }

        public ushort GetLabel(string name)
        {
            return _labels[name];
        }

        public byte[] ToArray() => _builder.ToArray();
    }
}