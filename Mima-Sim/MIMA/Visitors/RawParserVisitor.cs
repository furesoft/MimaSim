using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Visitors
{
    public class RawParserVisitor : INodeVisitor, IEmitter
    {
        private List<byte> _raw = new List<byte>();

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
                    _raw.Add((byte)lit.Value);
                }
            }
        }
    }
}