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

        private Stack<LiteralNode> _expressionStack = new();

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
                if (call.Name == "BinaryExpression")
                {
                    TraverseTree(call);
                    EmitExpression();
                }
                else if (call.Name == "if")
                {
                    VisitIfStatement(call);
                }
                else if (call.Name == "registerDefinition")
                {
                    VisitRegisterDefinition(call);
                }
                else if (call.Name == "varDefinition")
                {
                    VisitVarDefinition(call);
                }
                else if (call.Name == "{}")
                {
                    foreach (var line in call.Args)
                    {
                        Visit((CallNode)line);
                    }
                }
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
                var f = _expressionStack.Pop();
                EmitLiteral(f);

                if (_expressionStack.Count > 0)
                {
                    var s = _expressionStack.Pop();
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
                _expressionStack.Push(lit);
            }
            else if (ast is CallNode cn)
            {
                _opStack.Push(cn.Name);

                TraverseTree(cn.Args.First());
                TraverseTree(cn.Args.Last());
            }
        }

        private void VisitIfStatement(CallNode call)
        {
            // Emit Condition
            Visit((CallNode)call.Args.First());

            //todo: emit if-statement
            var trueLabel = _emitter.DefineLabel();

            _emitter.EmitInstruction(OpCodes.JMPC, (ushort)trueLabel.LabelNum);
            foreach (CallNode body in ((CallNode)call.Args.Last()).Args)
            {
                Visit(body);
            }
            _emitter.MarkLabel(trueLabel);
        }

        private void VisitRegisterDefinition(CallNode call)
        {
            var registerNode = (LiteralNode)call.Args.First();
            var valueNode = (LiteralNode)call.Args.Last();

            _emitter.EmitInstruction(OpCodes.LOAD, (ushort)valueNode.Value);
            _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, (Registers)registerNode.Value);
        }

        private void VisitVarDefinition(CallNode call)
        {
            var idNode = (IdentifierNode)call.Args.First();
            var adress = MemoryAllocator.Allocate(idNode.Name);

            var valueNode = call.Args.Last();

            if (valueNode is IdentifierNode valueIdNode)
            {
                var valueAddress = MemoryAllocator.Allocate(valueIdNode.Name);

                _emitter.EmitInstruction(OpCodes.MOV_MEM_MEM);
                _emitter.EmitLiteral(valueAddress);
                _emitter.EmitLiteral(adress);
            }
            else if (valueNode is CallNode regNode && regNode.Name == "registerExpression")
            {
                _emitter.EmitInstruction(OpCodes.MOV_REG_REG, (Registers)((LiteralNode)regNode.Args.First()).Value, Registers.Accumulator);
                _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                _emitter.EmitRegister(Registers.Accumulator);
                _emitter.EmitLiteral(adress);
            }
            else if (valueNode is CallNode cn)
            {
                Visit(cn);
            }
            else if (valueNode is LiteralNode litNode)
            {
                _emitter.EmitInstruction(OpCodes.LOAD, (ushort)litNode.Value);
                _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                _emitter.EmitRegister(Registers.Accumulator);
                _emitter.EmitLiteral(adress);
            }
        }
    }
}