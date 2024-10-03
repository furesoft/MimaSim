using System;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.Emiting;
using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Nodes.Operators;
using LiteralNode = Silverfly.Nodes.LiteralNode;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyVisitor : NodeVisitor, IEmitter
{
    private readonly ByteCodeEmitter _emitter = new();

    public AssemblyVisitor()
    {
        For<BlockNode>(Visit);
        For<InstructionNode>(Visit);
        For<LiteralNode>(VisitLiteral, node => node.Value is ulong);
        For<LiteralNode>(VisitRegister, node => node.Value is Registers);
        For<PrefixOperatorNode>(VisitAddress, op => op.Operator == "&");
    }

    private void VisitAddress(PrefixOperatorNode obj)
    {
        if (obj.Expr is LiteralNode literalNode && literalNode.Value is short addr)
        {
            _emitter.EmitLiteral(addr);
        }
    }

    public override void Visit(BlockNode block)
    {
        foreach (var child in block.Children)
        {
            child.Accept(this);
        }
    }

    public void VisitLiteral(LiteralNode lit)
    {
        _emitter.EmitLiteral(Convert.ToInt16(lit.Value));
    }

    public void VisitRegister(LiteralNode lit)
    {
        _emitter.EmitRegister((Registers)lit.Value);
    }

    public void Visit(InstructionNode instruction)
    {
        _emitter.EmitOpcode(SelectOpCode(instruction));

        foreach (var arg in instruction.Args)
        {
            arg.Accept(this);
        }
    }

    private OpCodes SelectOpCode(InstructionNode instruction)
    {
        return instruction.Mnemnonic switch
        {
            Mnemnonics.MOV => SelectMovOpCode(instruction),
            Mnemnonics.LOAD => OpCodes.LOAD,
            Mnemnonics.JMP => OpCodes.JMP,
            Mnemnonics.JMPC => OpCodes.JMPC,
            Mnemnonics.CMPNE => OpCodes.CMPNEQ,
            Mnemnonics.CMPE => OpCodes.CMPEQ,
            Mnemnonics.CMPLT => OpCodes.CMPLT,
            Mnemnonics.CMPGT => OpCodes.CMPGT,
            Mnemnonics.ADD => OpCodes.ADD,
            Mnemnonics.SUB => OpCodes.SUB,
            _ => throw new ArgumentOutOfRangeException(nameof(instruction.Mnemnonic), $@"No opcode defined for mnemonic: {instruction.Mnemnonic}")
        };
    }

    private OpCodes SelectMovOpCode(InstructionNode instruction)
    {
        if (IsRegisterArg(instruction.Args[0]) && IsRegisterArg(instruction.Args[1]))
        {
            return OpCodes.MOV_REG_REG;
        }

        if (IsRegisterArg(instruction.Args[0]) && IsMemArg(instruction.Args[1]))
        {
            return OpCodes.MOV_REG_MEM;
        }
        if (IsMemArg(instruction.Args[0]) && IsRegisterArg(instruction.Args[1]))
        {
            return OpCodes.MOV_REG_MEM;
        }
        if (IsMemArg(instruction.Args[0]) && IsMemArg(instruction.Args[1]))
        {
            return OpCodes.MOV_MEM_MEM;
        }

        throw new InvalidOperationException("Unknown opcode");
    }

    private static bool IsRegisterArg(AstNode node)
    {
        return node is LiteralNode { Value: Registers };
    }

    private static bool IsMemArg(AstNode node)
    {
        return node is PrefixOperatorNode p && p.Operator == "&";
    }

    public byte[] GetRaw()
    {
        return _emitter.ToArray();
    }
}