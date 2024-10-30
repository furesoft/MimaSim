using System;
using System.IO;
using System.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class IndentedTextWriter(TextWriter writer, string indentString = "    ") : TextWriter
{
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));
    private int _indentLevel = 0;

    public override Encoding Encoding => _writer.Encoding;

    public int IndentLevel
    {
        get => _indentLevel;
        set => _indentLevel = value < 0 ? 0 : value;
    }

    public void Indent() => IndentLevel++;

    public void Unindent() => IndentLevel--;

    public override void WriteLine(string? value)
    {
        WriteIndentation();
        _writer.WriteLine(value);
    }

    public override void Write(string? value)
    {
        WriteIndentation();
        _writer.Write(value);
    }

    private void WriteIndentation()
    {
        for (int i = 0; i < IndentLevel; i++)
        {
            _writer.Write(indentString);
        }
    }
}