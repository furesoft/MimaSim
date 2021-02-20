using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.Core.Emiting;
using System.Linq;

namespace MimaSim.MIMA.Visitors
{
    public class AssemblyVisitor : INodeVisitor, IEmitter
    {
        private ByteArrayBuilder _raw = new ByteArrayBuilder();

        public byte[] GetRaw()
        {
            return _raw.ToArray();
        }

        public void Visit(LiteralNode lit)
        {
            _raw.Append((ushort)lit.Value);
        }

        public void Visit(IdentifierNode id)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(CallNode call)
        {
            foreach (var arg in call.Args)
            {
                if (arg is CallNode cn)
                {
                    if (cn.Name == "noArgInstruction")
                    {
                        VisitNoArgInstruction(cn);
                    }
                    else if (cn.Name == "load")
                    {
                        _raw.Append((byte)OpCodes.LOAD);

                        //visit literal
                        var lit = (LiteralNode)cn.Args.First();

                        Visit(lit);
                    }
                }
            }
        }

        private void VisitNoArgInstruction(CallNode cn)
        {
            var opcodeNode = (LiteralNode)cn.Args.First();
            var opcode = (OpCodes)opcodeNode.Value;

            _raw.Append((byte)opcode);
        }
    }
}