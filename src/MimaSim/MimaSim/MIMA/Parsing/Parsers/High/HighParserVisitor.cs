using System.IO;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParserVisitor : NodeVisitor, IEmitter
{
    private readonly SymbolMap _symbolMap;
    private readonly IndentedTextWriter _writer;
    private readonly StringWriter _stringWriter;

    public HighParserVisitor(SymbolMap symbolMap)
    {
        _symbolMap = symbolMap;
        _stringWriter = new StringWriter();
        _writer = new IndentedTextWriter(_stringWriter);

        For<AsmNode>(VisitAsm);
        For<FuncDefNode>(VisitFuncDef);
    }

    private void VisitFuncDef(FuncDefNode def)
    {
        _writer.IndentLevel++;

        _writer.Write($"__{def.Name}__:");

        foreach (var node in def.Body)
        {
            node.Accept(this);
        }

        _writer.IndentLevel--;
    }

    private void VisitAsm(AsmNode asm)
    {
        _writer.WriteLine(asm.Source);
    }

    public byte[] GetRaw()
    {
        var assembler = new Assembler.AssemblySourceTextTranslator();

        return assembler.ToRaw(_stringWriter.ToString(), out var document);
    }
}