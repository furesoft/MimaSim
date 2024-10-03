using System;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.Emiting;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.Raw;

public class RawParserVisitor : NodeVisitor, IEmitter
{
    private readonly ByteArrayBuilder _raw = new();

    public RawParserVisitor()
    {
        For<LiteralNode>(Visit);
        For<BlockNode>(Visit);
    }

    public byte[] GetRaw()
    {
        return _raw.ToArray();
    }

    private void Visit(LiteralNode literal)
    {
        _raw.Append(Convert.ToByte(literal.Token.Text.ToString(), 16));
    }

    public override void Visit(BlockNode block)
    {
        foreach (var child in block.Children)
        {
            child.Accept(this);
        }
    }
}