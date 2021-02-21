using MimaSim.MIMA;
using System;

namespace MimaSim.Core.Emiting
{
    public class ByteCodeEmitter
    {
        private ByteArrayBuilder _builder = new ByteArrayBuilder();

        public int Position => _builder.Length;

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

        public byte[] ToArray() => _builder.ToArray();
    }
}