using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Shifting
{
    public class RsfRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RSHIFT;

        public bool Invoke(CPU cpu)
        {
            var oldValue = cpu.GetRegister(Registers.X);
            var shiftBy = cpu.GetRegister(Registers.Y);
            var res = oldValue >> shiftBy;

            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}