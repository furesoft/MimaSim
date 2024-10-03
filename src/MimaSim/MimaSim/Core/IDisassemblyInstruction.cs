using System.Text;
using MimaSim.MIMA;

namespace MimaSim.Core;

public interface IDisassemblyInstruction
{
    OpCodes OpCode { get; }

    void Dissassemble(StringBuilder builder, Disassembler disassembler);
}