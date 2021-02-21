using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class DivInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.DIV;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.GetRegister(Registers.X);
            var r2 = cpu.GetRegister(Registers.Y);

            cpu.SetRegister(Registers.Accumulator, (ushort)(r1 / r2));

            return false;
        }
    }
}