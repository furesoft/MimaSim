using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using MimaSim.MIMA.Parsing;
using System;

namespace MimaSim.MIMA.Visitors
{
    public class HighParserVisitor : INodeVisitor, IEmitter
    {
        private readonly ByteCodeEmitter _emitter = new ByteCodeEmitter();
        private readonly RegisterAllocator _registerAllocator = new RegisterAllocator();

        public byte[] GetRaw()
        {
            return _emitter.ToArray();
        }

        public void Visit(LiteralNode lit)
        {
            _emitter.EmitInstruction(OpCodes.LOAD);
            _emitter.EmitLiteral((ushort)lit.Value);
        }

        public void Visit(IdentifierNode id)
        {
            throw new NotImplementedException();
        }

        public void Visit(CallNode call)
        {
            if (!call.IsEmpty)
            {
                foreach (var arg in call.Args)
                {
                    if (arg is LiteralNode lit)
                    {
                        Visit(lit);
                        Registers register = _registerAllocator.Allocate();

                        _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, register);

                        if (register == Registers.Y)
                        {
                            goto emitOpcode;
                        }
                    }
                    else if (arg is CallNode cn)
                    {
                        Visit(cn);
                    }
                }

            emitOpcode:
                switch (call.Name)
                {
                    case "+":
                        _emitter.EmitInstruction(OpCodes.ADD);
                        break;

                    case "-":
                        _emitter.EmitInstruction(OpCodes.SUB);
                        break;

                    case "*":
                        _emitter.EmitInstruction(OpCodes.MUL);
                        break;

                    case "/":
                        _emitter.EmitInstruction(OpCodes.DIV);
                        break;
                }
            }
        }
    }
}