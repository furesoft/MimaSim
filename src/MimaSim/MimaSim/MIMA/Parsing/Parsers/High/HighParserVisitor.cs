using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;
using Silverfly.Nodes;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParserVisitor : TaggedNodeVisitor<Scope>, IEmitter
{
    private readonly IndentedTextWriter _writer;
    private readonly StringWriter _stringWriter;
    private readonly MemoryAllocator Allocator = new();

    public HighParserVisitor()
    {
        _stringWriter = new StringWriter();
        _writer = new IndentedTextWriter(_stringWriter);

        For<BlockNode>(VisitBlock);
        For<AsmNode>(VisitAsm);
        For<FuncDefNode>(VisitFuncDef);
        For<LiteralNode>(VisitLiteral);
        For<ReturnStatement>(VisitReturn);
        For<CallNode>(VisitCall);

        _writer.WriteLine("jmp $__main__");
    }

    private void VisitCall(CallNode call, Scope scope)
    {
        var token = ((NameNode)call.FunctionExpr).Token;
        var symbol = scope.Get(token);

        if (symbol == null)
        {
            call.AddMessage(MessageSeverity.Error, "Symbol not found");
            return;
        }

        if (symbol is not FunctionSymbol)
        {
            call.AddMessage(MessageSeverity.Error, "Symbol is not a function");
            return;
        }

        foreach (var argument in call.Arguments)
        {
            argument.Accept(this, scope);
        }

        _writer.WriteLine("call $" + NameMangler.Mangle(symbol));
    }

    private void VisitBlock(BlockNode obj, Scope scope)
    {
        foreach (var node in obj.Children)
        {
            node.Accept(this, scope);
        }
    }

    private void VisitReturn(ReturnStatement obj, Scope scope)
    {
        if (obj.Value is null)
        {
            return;
        }

        obj.Value.Accept(this, scope);
        _writer.WriteLine("push");
        _writer.WriteLine("ret");
    }

    private void VisitLiteral(LiteralNode literal, Scope scope)
    {
        switch (literal.Value)
        {
            case bool b:
                _writer.WriteLine("load " + (b ? "1" : "0"));
                break;
            case ulong b:
                _writer.WriteLine($"load {b}");
                break;
        }

    }

    private void VisitFuncDef(FuncDefNode def, Scope scope)
    {
        var funcScope = scope.NewSubScope();

        foreach (var parameter in def.Parameters.OfType<ParameterNode>())
        {
            funcScope.Define(new ParameterSymbol(parameter.Name, parameter.Type));
        }

        var funcSymbol = scope.Get(def.Name);

        _writer.WriteLine(NameMangler.Mangle(funcSymbol!));
        _writer.IndentLevel++;
        foreach (var node in def.Body)
        {
            if (def.Name.ToString() == "main" && node is ReturnStatement)
            {
                _writer.WriteLine("exit");
                continue;
            }

            node.Accept(this, scope);
        }

        _writer.IndentLevel--;
    }

    private void VisitAsm(AsmNode asm, Scope scope)
    {
        _writer.WriteLine(asm.Source.Replace("\t", ""));
    }

    public byte[] GetRaw()
    {
        var assembler = new Assembler.AssemblySourceTextTranslator();

        var asm = _stringWriter.ToString();
        Console.WriteLine(asm);
        Debug.WriteLine(asm);

        return assembler.ToRaw(asm, out var document);
    }
}