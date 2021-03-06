using MimaSim.MIMA;
using System.Collections.Generic;

namespace MimaSim.Core.Parsing.Emiting
{
    public class ByteCodeEmitter
    {
        private readonly ByteArrayBuilder _builder = new ByteArrayBuilder();

        private readonly Dictionary<string, byte> _labels = new Dictionary<string, byte>();
        private int _labelCount = 0;
        public int Position => _builder.Length;

        public void Append(byte[] raw)
        {
            _builder.Append(raw, false);
        }

        public Label DefineLabel()
        {
            return new Label(_labelCount++);
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

        public void EmitInstruction(OpCodes opcode, Registers reg1, byte address)
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

        public byte GetLabel(string name)
        {
            return _labels[name];
        }

        public void MarkLabel(Label label)
        {
            _labels.Add("L" + label.LabelNum, (byte)_builder.Length);
        }

        public byte[] ToArray() => _builder.ToArray();

        internal void CreateLabel(string name)
        {
            _labels.Add(name, (byte)_builder.Length);
        }
    }
}