using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.Core.Parsing.Emiting;
using MimaSim.MIMA.Parsing;
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
            _emitter.EmitLiteral((short)lit.Value);
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
                    if (cn.Type == AstCallNodeType.NoArgInstruction)
                    {
                        VisitNoArgInstruction(cn);
                    }
                    else if (cn.Type == AstCallNodeType.Load)
                    {
                        _emitter.EmitOpcode(OpCodes.LOAD);

                        var lit = (LiteralNode)cn.Args.First();

                        Visit(lit);
                    }
                    else if (cn.Type == AstCallNodeType.MovRegReg)
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
                    else if (cn.Type == AstCallNodeType.MovRegMem)
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is Registers reg && secondArg.Value is short addr)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_REG_MEM);

                            _emitter.EmitRegister(reg);
                            _emitter.EmitLiteral(addr);
                        }
                    }
                    else if (cn.Type == AstCallNodeType.MovMemReg)
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is short addr && secondArg.Value is Registers reg)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_MEM_REG);

                            _emitter.EmitLiteral(addr);
                            _emitter.EmitRegister(reg);
                        }
                    }
                    else if (cn.Type == AstCallNodeType.MovMemMem)
                    {
                        var firstArg = (LiteralNode)cn.Args.First();
                        var secondArg = (LiteralNode)cn.Args.Last();

                        if (firstArg.Value is byte addr1 && secondArg.Value is byte addr2)
                        {
                            _emitter.EmitOpcode(OpCodes.MOV_MEM_MEM);

                            _emitter.EmitLiteral(addr1);
                            _emitter.EmitLiteral(addr2);
                        }
                    }
                    else if (cn.Type == AstCallNodeType.Jmp)
                    {
                        var addressArg = (LiteralNode)cn.Args.First();
                        var address = _emitter.GetLabel(addressArg.Value.ToString().Remove(0, 1));

                        _emitter.EmitOpcode(OpCodes.JMP);
                        _emitter.EmitLiteral(address);
                    }
                    else if (cn.Type == AstCallNodeType.Label)
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