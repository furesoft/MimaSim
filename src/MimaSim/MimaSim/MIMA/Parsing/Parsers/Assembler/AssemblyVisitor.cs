using System;
using System.Collections.Generic;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.Emiting;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.Parsing.Parsers.Assembler.AST;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Nodes.Operators;
using Silverfly.Text;
using LiteralNode = Silverfly.Nodes.LiteralNode;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyVisitor : TaggedNodeVisitor<Scope>, IEmitter
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
        For<NameNode>(VisitArg);

        foreach (var register in Enum.GetNames<Registers>())
        {
            Scope.Root.Define(register.ToLower(), Enum.Parse<Registers>(register));
        }

        foreach (var color in Enum.GetNames<DisplayColor>())
        {
            Scope.Root.Define(color.ToUpper(), (short)Enum.Parse<DisplayColor>(color));
        }

        foreach (var flag in Enum.GetNames<Flags>())
        {
            Scope.Root.Define(flag.ToUpper(), (short)Enum.Parse<Flags>(flag));
        }

        foreach (var syscall in Enum.GetNames<SysCall>())
        {
            Scope.Root.Define(syscall.ToUpper(), (short)Enum.Parse<SysCall>(syscall));
        }
    }

    private void VisitMacroInvocation(InstructionNode invocation, Scope scope)
    {
        if (_macros.TryGetValue(invocation.Mnemnonic.Text.ToString(), out var macro))
        {
            if (invocation.Args.Count != macro.Parameters.Count)
            {
                throw new ArgumentException($"Macro '{macro.NameToken.Text}' expects {macro.Parameters.Count} arguments but received {invocation.Args.Count}.");
            }

            var subscope = scope.NewSubScope();

            for (int i = 0; i < macro.Parameters.Count; i++)
            {
                var paramName = ((NameNode)macro.Parameters[i]).Token.Text.ToString();
                var arg = GetArgumentValue(invocation.Args[i], subscope);

                subscope.Define(paramName, arg);
            }

            foreach (var node in macro.Body)
            {
                node.Accept(this, subscope);
            }
        }
        else
        {
            invocation.AddMessage(MessageSeverity.Error, $"Undefined macro: {invocation.Mnemnonic.Text}");
        }
    }

    private static object? GetArgumentValue(AstNode node, Scope scope)
    {
        return node switch
        {
            LiteralNode n => n.Value,
            NameNode name => scope.Get(name.Token.Text.ToString()),
            _ => throw new ArgumentException($"Undefined argument: {node}"),
        };
    }

    private void VisitMacroDefinition(MacroNode macro, Scope scope)
    {
        _macros.TryAdd(macro.NameToken.Text.ToString(), macro);
    }

    private void VisitLabel(PostfixOperatorNode labelDef, Scope scope)
    {
        if (labelDef.Expr is NameNode nameNode)
        {
            _emitter.CreateLabel(nameNode.Token.Text.ToString());
        }
    }

    private Dictionary<string, short> _labelAddressMap = new();
    private List<(int, string)> _unresolvedLabels = new();

    private void VisitLabelRef(PrefixOperatorNode labelRef, Scope scope)
    {
        if (labelRef.Expr is NameNode nameNode)
        {
            var labelName = nameNode.Token.Text.ToString();

            if (_labelAddressMap.TryGetValue(labelName, out var address))
            {
                _emitter.EmitLiteral(address);
            }
            else
            {
                _unresolvedLabels.Add((_emitter.Position, labelName));
                _emitter.EmitLiteral(0);
            }
        }
    }

    private void VisitAddress(PrefixOperatorNode obj, Scope scope)
    {
        if (obj.Expr is LiteralNode literalNode && literalNode.Value is short addr)
        {
            _emitter.EmitLiteral(addr);
        }
    }

    public override void Visit(BlockNode block, Scope scope)
    {
        foreach (var child in block.Children)
        {
            child.Accept(this, scope);
        }
    }

    public void VisitLiteral(LiteralNode lit, Scope scope)
    {
        _emitter.EmitLiteral(Convert.ToInt16(lit.Value));
    }

    public void VisitRegister(LiteralNode lit, Scope scope)
    {
        _emitter.EmitRegister((Registers)lit.Value);
    }

    public void VisitArg(NameNode arg, Scope scope)
    {
       var value = scope.Get(arg.Token.Text.ToString());

       switch (value)
       {
           case Registers r:
               _emitter.EmitRegister(r);
               break;
           case ulong v:
               _emitter.EmitLiteral(Convert.ToInt16(v));
               break;
           case short s:
               _emitter.EmitLiteral(s);
               break;
           case string s:
               _emitter.EmitLiteral(Convert.ToInt16(s[0]));
               break;
           default:
               throw new InvalidOperationException();
       }
    }

    public void Visit(InstructionNode instruction, Scope scope)
    {
        if (Enum.TryParse<Mnemnonics>(instruction.Mnemnonic.Text.ToString(), true, out var mnemnonic))
        {
            _emitter.EmitOpcode(SelectOpCode(instruction, scope, mnemnonic));

            foreach (var arg in instruction.Args)
            {
                arg.Accept(this, scope);
            }
        }
        else
        {
            VisitMacroInvocation(instruction, scope);
        }
    }

    private OpCodes SelectOpCode(InstructionNode instruction, Scope scope, Mnemnonics mnemnonic)
    {
        if (mnemnonic == Mnemnonics.MOV)
        {
            return SelectMovOpCode(instruction, scope);
        }

        return Enum.Parse<OpCodes>(mnemnonic.ToString());
    }

    private OpCodes SelectMovOpCode(InstructionNode instruction, Scope scope)
    {
        if (IsRegisterArg(instruction.Args[0], scope) && IsRegisterArg(instruction.Args[1], scope))
        {
            return OpCodes.MOV_REG_REG;
        }

        if (IsRegisterArg(instruction.Args[0], scope) && IsMemArg(instruction.Args[1]))
        {
            return OpCodes.MOV_REG_MEM;
        }
        if (IsMemArg(instruction.Args[0]) && IsRegisterArg(instruction.Args[1], scope))
        {
            return OpCodes.MOV_REG_MEM;
        }
        if (IsMemArg(instruction.Args[0]) && IsMemArg(instruction.Args[1]))
        {
            return OpCodes.MOV_MEM_MEM;
        }

        throw new InvalidOperationException("Unknown opcode");
    }

    private static bool IsRegisterArg(AstNode node, Scope scope)
    {
        var name = ((NameNode)node).Token.Text.ToString();

        return scope.Get(name.ToLower()) is Registers;
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