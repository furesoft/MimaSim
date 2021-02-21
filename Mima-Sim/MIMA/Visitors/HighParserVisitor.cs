using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using MimaSim.MIMA.Parsing;
using System;
using System.Linq;

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
            _emitter.EmitInstruction(OpCodes.LOAD, (ushort)lit.Value);
        }

        public void Visit(IdentifierNode id)
        {
            throw new NotImplementedException();
        }

        public void Visit(CallNode call)
        {
            if (!call.IsEmpty)
            {
                TraverseTree(call);
            }
        }

        private void EmitArithmeticOperators(CallNode call)
        {
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

                case "!":
                    _emitter.EmitInstruction(OpCodes.NOT);
                    break;

                case "|":
                    _emitter.EmitInstruction(OpCodes.OR);
                    break;

                case "^":
                    _emitter.EmitInstruction(OpCodes.XOR);
                    break;

                case "&":
                    _emitter.EmitInstruction(OpCodes.AND);
                    break;

                case ">":
                    _emitter.EmitInstruction(OpCodes.CMPGT);
                    break;

                case "<":
                    _emitter.EmitInstruction(OpCodes.CMPLT);
                    break;

                case "==":
                    _emitter.EmitInstruction(OpCodes.CMPEQ);
                    break;

                case "!=":
                    _emitter.EmitInstruction(OpCodes.CMPNEQ);
                    break;

                case "<=":
                    _emitter.EmitInstruction(OpCodes.CMPGE);
                    break;

                case ">=":
                    _emitter.EmitInstruction(OpCodes.CMPGE);
                    break;
            }
        }

        private void TraverseTree(IAstNode ast)
        {
            if (ast is LiteralNode lit)
            {
                Visit(lit);
                Registers register = _registerAllocator.Allocate();

                _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, register);
            }
            else if (ast is CallNode cn)
            {
                TraverseTree(cn.Args.First());
                TraverseTree(cn.Args.Last());

                EmitArithmeticOperators(cn);
            }
        }
    }
}