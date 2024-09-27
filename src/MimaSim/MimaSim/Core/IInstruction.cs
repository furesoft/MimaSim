using MimaSim.MIMA;
using MimaSim.MIMA.Components;

namespace MimaSim.Core;

public interface IInstruction
{
    OpCodes Instruction { get; }

    bool Invoke(CPU cpu);
}