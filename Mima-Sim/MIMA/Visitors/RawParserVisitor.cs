using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Visitors
{
    public class RawParserVisitor : INodeVisitor, IEmitter
    {
        private ByteArrayBuilder _raw = new ByteArrayBuilder();

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
}