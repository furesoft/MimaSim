using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.Core.Parsing.Emiting;
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

        private Stack<AstCallNodeType> _opStack = new();

        public byte[] GetRaw()
        {
            return _emitter.ToArray();
        }

        public void Visit(LiteralNode lit)
        {
            _emitter.EmitInstruction(OpCodes.LOAD, (byte)lit.Value);
        }

        public void Visit(IdentifierNode id)
        {
            throw new NotImplementedException();
        }

        public void Visit(CallNode call)
        {
            if (!call.IsEmpty)
            {
                switch (call.Type)
                {
                    case AstCallNodeType.BinaryExpresson:
                        TraverseTree(call);
                        EmitExpression();
                        break;

                    case AstCallNodeType.IfStatement:
                        VisitIfStatement(call);
                        break;

                    case AstCallNodeType.RegisterDefinitionStatement:
                        VisitRegisterDefinition(call);
                        break;

                    case AstCallNodeType.VariableDefinitionStatement:
                        VisitVarDefinition(call);
                        break;

                    case AstCallNodeType.VariableAssignmentStatement:
                        VisitVarAssignment(call);
                        break;

                    case AstCallNodeType.Group:
                        {
                            foreach (var line in call.Args)
                            {
                                Visit((CallNode)line);
                            }

                            break;
                        }
                }
            }
        }

        private void EmitArithmeticOperator(AstCallNodeType op)
        {
            switch (op)
            {
                case AstCallNodeType.Addition:
                    _emitter.EmitInstruction(OpCodes.ADD);
                    break;

                case AstCallNodeType.Subtraktion:
                    _emitter.EmitInstruction(OpCodes.SUB);
                    break;

                case AstCallNodeType.Multiplication:
                    _emitter.EmitInstruction(OpCodes.MUL);
                    break;

                case AstCallNodeType.Division:
                    _emitter.EmitInstruction(OpCodes.DIV);
                    break;

                case AstCallNodeType.Not:
                    _emitter.EmitInstruction(OpCodes.NOT);
                    break;

                case AstCallNodeType.Or:
                    _emitter.EmitInstruction(OpCodes.OR);
                    break;

                case AstCallNodeType.Xor:
                    _emitter.EmitInstruction(OpCodes.XOR);
                    break;

                case AstCallNodeType.And:
                    _emitter.EmitInstruction(OpCodes.AND);
                    break;

                case AstCallNodeType.GreaterThan:
                    _emitter.EmitInstruction(OpCodes.CMPGT);
                    break;

                case AstCallNodeType.LessThen:
                    _emitter.EmitInstruction(OpCodes.CMPLT);
                    break;

                case AstCallNodeType.Equal:
                    _emitter.EmitInstruction(OpCodes.CMPEQ);
                    break;

                case AstCallNodeType.NotEqual:
                    _emitter.EmitInstruction(OpCodes.CMPNEQ);
                    break;

                case AstCallNodeType.LessEqual:
                    _emitter.EmitInstruction(OpCodes.CMPGE);
                    break;

                case AstCallNodeType.GreaterEqual:
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
                _opStack.Push(cn.Type);

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

            _emitter.EmitInstruction(OpCodes.JMPC, (byte)trueLabel.LabelNum);
            foreach (CallNode body in ((CallNode)call.Args.Last()).Args)
            {
                Visit(body);
            }
            _emitter.MarkLabel(trueLabel);
        }

        private void VisitRegisterDefinition(CallNode call)
        {
            var registerNode = (LiteralNode)call.Args.First();
            var valueNode = call.Args.Last();

            if (valueNode is LiteralNode ln)
            {
                _emitter.EmitInstruction(OpCodes.LOAD, (byte)ln.Value);
                _emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, (Registers)registerNode.Value);
            }
            else if (valueNode is IdentifierNode idNode)
            {
                var address = MemoryAllocator.Allocate(idNode.Name);
                _emitter.EmitInstruction(OpCodes.MOV_MEM_REG);
                _emitter.EmitLiteral(address);
                _emitter.EmitRegister((Registers)registerNode.Value);
            }
            else if (valueNode is CallNode cn)
            {
                if (cn.Type == AstCallNodeType.AddressOfExpression)
                {
                    var addressNode = cn.Args.First();

                    if (addressNode is IdentifierNode nameNode)
                    {
                        var address = MemoryAllocator.Allocate(nameNode.Name);

                        _emitter.EmitInstruction(OpCodes.LOAD, address);
                        _emitter.EmitInstruction(OpCodes.MOV_REG_REG);
                        _emitter.EmitRegister(Registers.Accumulator);
                        _emitter.EmitRegister((Registers)registerNode.Value);
                    }
                }
            }
        }

        private void VisitVarAssignment(CallNode call)
        {
            var idNode = (IdentifierNode)call.Args.First();
            var value = call.Args.Last();
            var memoryAddress = MemoryAllocator.Allocate(idNode.Name);

            if (value is LiteralNode valueNode)
            {
                _emitter.EmitInstruction(OpCodes.LOAD, (byte)valueNode.Value);
            }
            else if (value is CallNode cn)
            {
                if (cn.Type == AstCallNodeType.RegisterExpression)
                {
                    var reg = (LiteralNode)cn.Args.First();

                    _emitter.EmitInstruction(OpCodes.MOV_REG_REG);
                    _emitter.EmitRegister((Registers)reg.Value);
                    _emitter.EmitRegister(Registers.Accumulator);
                }
                else if (cn.Type == AstCallNodeType.AddressOfExpression)
                {
                    var addressNode = cn.Args.First();

                    if (addressNode is IdentifierNode nameNode)
                    {
                        var address = MemoryAllocator.Allocate(nameNode.Name);

                        _emitter.EmitInstruction(OpCodes.LOAD, address);
                        _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                        _emitter.EmitRegister(Registers.Accumulator);
                        _emitter.EmitLiteral(memoryAddress);
                    }
                }
            }

            _emitter.EmitInstruction(OpCodes.MOV_REG_MEM, Registers.Accumulator, memoryAddress);
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
            else if (valueNode is CallNode regNode)
            {
                if (regNode.Type == AstCallNodeType.RegisterExpression)
                {
                    _emitter.EmitInstruction(OpCodes.MOV_REG_REG, (Registers)((LiteralNode)regNode.Args.First()).Value, Registers.Accumulator);
                    _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                    _emitter.EmitRegister(Registers.Accumulator);
                    _emitter.EmitLiteral(adress);
                }
                else if (regNode.Type == AstCallNodeType.AddressOfExpression)
                {
                    var addressNode = regNode.Args.First();

                    if (addressNode is IdentifierNode nameNode)
                    {
                        var address = MemoryAllocator.Allocate(nameNode.Name);

                        _emitter.EmitInstruction(OpCodes.LOAD, address);
                        _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                        _emitter.EmitRegister(Registers.Accumulator);
                        _emitter.EmitLiteral(adress);
                    }
                }
            }
            else if (valueNode is CallNode cn)
            {
                Visit(cn);
            }
            else if (valueNode is LiteralNode litNode)
            {
                _emitter.EmitInstruction(OpCodes.LOAD, (byte)litNode.Value);
                _emitter.EmitInstruction(OpCodes.MOV_REG_MEM);
                _emitter.EmitRegister(Registers.Accumulator);
                _emitter.EmitLiteral(adress);
            }
        }
    }
}