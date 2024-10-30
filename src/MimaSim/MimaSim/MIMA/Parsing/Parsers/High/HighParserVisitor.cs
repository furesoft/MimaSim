using System.IO;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.AST;
using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParserVisitor : NodeVisitor, IEmitter
{
    private readonly IndentedTextWriter _writer;
    private readonly StringWriter _stringWriter;

    public HighParserVisitor()
    {
        _stringWriter = new StringWriter();
        _writer = new IndentedTextWriter(_stringWriter);

        For<AsmNode>(VisitAsm);
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