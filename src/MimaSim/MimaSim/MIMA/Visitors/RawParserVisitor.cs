using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.Core.Parsing.Emiting;
using System;

namespace MimaSim.MIMA.Visitors;

public class RawParserVisitor : INodeVisitor, IEmitter
{
    private readonly ByteArrayBuilder _raw = new();

    public byte[] GetRaw()
    {
        return _raw.ToArray();
    }

    public void Visit(LiteralNode lit)
    {
        throw new NotImplementedException();
    }

    public void Visit(IdentifierNode id)
    {
        throw new NotImplementedException();
    }

    public void Visit(CallNode call)
    {
        foreach (var arg in call.Args)
        {
            if (arg is LiteralNode lit)
            {
                _raw.Append((byte)lit.Value);
            }
        }
    }
}