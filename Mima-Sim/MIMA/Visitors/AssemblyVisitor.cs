using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using MimaSim.MIMA.VM;
using System.Linq;

namespace MimaSim.MIMA.Visitors
{
    public class AssemblyVisitor : INodeVisitor, IEmitter
    {
        private ByteCodeEmitter _emitter = new ByteCodeEmitter();
        private DiagnosticBag Diagnostics = new DiagnosticBag();

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
                        else
                        {
                            Diagnostics.ReportInvalidMovInstruction();
                        }
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