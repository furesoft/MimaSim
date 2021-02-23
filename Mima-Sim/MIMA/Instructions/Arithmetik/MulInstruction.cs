using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class MulInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MUL;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.GetRegister(Registers.X);
            var r2 = cpu.GetRegister(Registers.Y);

            cpu.SetRegister(Registers.Accumulator, (byte)(r1 * r2));

            return false;
        }
    }
}