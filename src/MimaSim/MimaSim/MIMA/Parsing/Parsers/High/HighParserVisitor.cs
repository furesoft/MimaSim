using System;
using System.Diagnostics;
using System.IO;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParserVisitor : NodeVisitor, IEmitter
{
    private readonly SymbolMap _symbolMap;
    private readonly IndentedTextWriter _writer;
    private readonly StringWriter _stringWriter;
    private readonly MemoryAllocator Allocator = new();

    public HighParserVisitor(SymbolMap symbolMap)
    {
        _symbolMap = symbolMap;
        _stringWriter = new StringWriter();
        _writer = new IndentedTextWriter(_stringWriter);

        For<AsmNode>(VisitAsm);
        For<FuncDefNode>(VisitFuncDef);
        For<LiteralNode>(VisitLiteral);
        For<ReturnStatement>(VisitReturn);
        For<BlockNode>(VisitBlock);

        _writer.WriteLine("jmp __main__");
    }

    private void VisitBlock(BlockNode obj)
    {
        foreach (var node in obj.Children)
        {
            node.Accept(this);
        }
    }

    private void VisitReturn(ReturnStatement obj)
    {
        if (obj.Value is null)
        {
            return;
        }

        obj.Value.Accept(this);
        _writer.WriteLine("push");
        _writer.WriteLine("ret");
    }

    private void VisitLiteral(LiteralNode obj)
    {
        switch (obj.Value)
        {
            case bool b:
                _writer.WriteLine("load " + (b ? "1" : "0"));
                break;
            case ulong b:
                _writer.WriteLine($"load {b}");
                break;
        }

    }

    private void VisitFuncDef(FuncDefNode def)
    {
        _writer.IndentLevel++;

        _writer.WriteLine($"__{def.Name}__:");

        foreach (var node in def.Body)
        {
            node.Accept(this);
        }

        _writer.IndentLevel--;

        if (def.Name == "main")
        {
            _writer.WriteLine("exit");
        }
    }

    private void VisitAsm(AsmNode asm)
    {
        _writer.WriteLine(asm.Source);
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