using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class IncReg : IInstruction
    {
        public OpCodes Instruction => OpCodes.INC;

        public bool Invoke(CPU cpu)
        {
            var oldValue = cpu.GetRegister(Registers.Accumulator);
            var newValue = oldValue + 1;

            cpu.SetRegister(Registers.Accumulator, (short)newValue);

            return false;
        }
    }
}