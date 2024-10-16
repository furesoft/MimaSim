using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.Emiting;
using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Nodes.Operators;
using Silverfly.Text;
using LiteralNode = Silverfly.Nodes.LiteralNode;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyVisitor : NodeVisitor, IEmitter
{
    private readonly ByteCodeEmitter _emitter = new();
    private Dictionary<string, MacroNode> _macros = new();

    public AssemblyVisitor()
    {
        For<BlockNode>(Visit);
        For<InstructionNode>(Visit);
        For<LiteralNode>(VisitLiteral, node => node.Value is ulong);
        For<LiteralNode>(VisitRegister, node => node.Value is Registers);
        For<PrefixOperatorNode>(VisitAddress, op => op.Tag == "address");
        For<PrefixOperatorNode>(VisitLabelRef, op => op.Tag == "labelref");
        For<PostfixOperatorNode>(VisitLabel, op => op.Tag == "label");
        For<MacroNode>(VisitMacroDefinition);
    }

    private void VisitMacroInvocation(MacroInvocationNode invocation)
    {
        if (_macros.TryGetValue(invocation.NameToken.Text.ToString(), out var macro))
        {
            if (invocation.Arguments.Count != macro.Parameters.Count)
            {
                throw new ArgumentException($"Macro '{macro.NameToken.Text}' expects {macro.Parameters.Count} arguments but received {invocation.Arguments.Count}.");
            }

            var parameterMap = new Dictionary<string, AstNode>();
            for (int i = 0; i < macro.Parameters.Count; i++)
            {
               /* var paramName = macro.Parameters[i].Text.ToString();
                var arg = invocation.Arguments[i];
                parameterMap[paramName] = arg;*/
            }

            ExpandMacroBody(macro.Body, parameterMap);
        }
        else
        {
            invocation.AddMessage(MessageSeverity.Error, $"Undefined macro: {invocation.NameToken.Text}");
        }
    }

    private void ExpandMacroBody(ImmutableList<AstNode> body, Dictionary<string, AstNode> parameterMap)
    {
        foreach (var child in body)
        {
            SubstituteParameters(child, parameterMap);
            child.Accept(this);
        }
    }

    private void SubstituteParameters(AstNode node, Dictionary<string, AstNode> parameterMap)
    {
        if (node is NameNode nameNode && parameterMap.TryGetValue(nameNode.Token.Text.ToString(), out var replacement))
        {
            //node.ReplaceWith(replacement);
        }
    }

    private void VisitMacroDefinition(MacroNode macro)
    {
        _macros.TryAdd(macro.NameToken.Text.ToString(), macro);
    }

    private void VisitLabel(PostfixOperatorNode labelDef)
    {
        if (labelDef.Expr is NameNode nameNode)
        {
            _emitter.CreateLabel(nameNode.Token.Text.ToString());
        }
    }

    private void VisitLabelRef(PrefixOperatorNode labelRef)
    {
        if (labelRef.Expr is NameNode nameNode)
        {
            //todo: emit 0 address and adjust later in GetRaw()
        }
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
            Mnemnonics.PUSH => OpCodes.PUSH,
            Mnemnonics.POP => OpCodes.POP,
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