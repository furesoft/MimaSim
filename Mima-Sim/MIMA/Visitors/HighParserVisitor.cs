using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using MimaSim.MIMA.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Visitors
{
    public class HighParserVisitor : INodeVisitor, IEmitter
    {
        private readonly ByteCodeEmitter _emitter = new();
        private readonly RegisterAllocator _registerAllocator = new();

        private Queue<LiteralNode> _expressionStack = new();

        private Stack<string> _opStack = new();

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
                EmitExpression();
            }
        }

        private void EmitArithmeticOperator(string op)
        {
            switch (op)
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

        private void EmitExpression()
        {
            while (_expressionStack.Count > 0)
            {
                var f = _expressionStack.Dequeue();
                EmitLiteral(f);

                if (_expressionStack.Count > 0)
                {
                    var s = _expressionStack.Dequeue();
                    EmitLiteral(s);
                }

                var op = _opStack.Pop();
                EmitArithmeticOperator(op);

                if (_opStack.Count > 0)
                {
                    _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, _registerAllocator.Allocate());
                }
            }
        }

        private void EmitLiteral(LiteralNode lit)
        {
            Visit(lit);
            Registers register = _registerAllocator.Allocate();

            _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, register);
        }

        private void TraverseTree(IAstNode ast)
        {
            if (ast is LiteralNode lit)
            {
                _expressionStack.Enqueue(lit);
            }
            else if (ast is CallNode cn)
            {
                _opStack.Push(cn.Name);

                TraverseTree(cn.Args.First());
                TraverseTree(cn.Args.Last());
            }
        }
    }
}