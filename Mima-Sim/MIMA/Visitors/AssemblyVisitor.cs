﻿using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Visitors
{
    public class AssemblyVisitor : INodeVisitor, IEmitter
    {
        private readonly ByteCodeEmitter _emitter = new ByteCodeEmitter();

        private readonly DiagnosticBag Diagnostics = new DiagnosticBag();

        public byte[] GetRaw()
        {
            return _emitter.ToArray();
        }

        public void Visit(LiteralNode lit)
        {
            _emitter.EmitLiteral((ushort)lit.Value);
        }

        public void Visit(IdentifierNode id)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(CallNode call)
        {
            foreach (var arg in call.Args)
            {
                if (arg is CallNode cn)
                {
                    if (cn.Name == "noArgInstruction")
                    {
                        VisitNoArgInstruction(cn);
                    }
                    else if (cn.Name == "load")
                    {
                        _emitter.EmitOpcode(OpCodes.LOAD);

                        var lit = (LiteralNode)cn.Args.First();

                        Visit(lit);
                    }
                    else if (cn.Name == "mov")
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is Registers reg1 && secondArg.Value is Registers reg2)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_REG_REG);

                            _emitter.EmitRegister(reg1);
                            _emitter.EmitRegister(reg2);
                        }
                    }
                    else if (cn.Name == "mov_reg_mem")
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is Registers reg && secondArg.Value is ushort addr)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_REG_MEM);

                            _emitter.EmitRegister(reg);
                            _emitter.EmitLiteral(addr);
                        }
                    }
                    else if (cn.Name == "mov_mem_reg")
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is ushort addr && secondArg.Value is Registers reg)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_MEM_REG);

                            _emitter.EmitLiteral(addr);
                            _emitter.EmitRegister(reg);
                        }
                    }
                    else if (cn.Name == "mov_mem_mem")
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is ushort addr1 && secondArg.Value is ushort addr2)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_MEM_MEM);

                            _emitter.EmitLiteral(addr1);
                            _emitter.EmitLiteral(addr2);
                        }
                    }
                    else if (cn.Name == "jmp")
                    {
                        var addressArg = (LiteralNode)cn.Args.First();
                        var address = _emitter.GetLabel(addressArg.Value.ToString().Remove(0, 1));

                        _emitter.EmitOpcode(OpCodes.JMP);
                        _emitter.EmitLiteral(address);
                    }
                    else if (cn.Name == "label")
                    {
                        var id = (IdentifierNode)cn.Args.First();

                        _emitter.CreateLabel(id.Name);
                    }
                }
            }
        }

        private void VisitNoArgInstruction(CallNode cn)
        {
            var opcodeNode = (LiteralNode)cn.Args.First();
            var opcode = (OpCodes)opcodeNode.Value;

            _emitter.EmitOpcode(opcode);
        }
    }
}