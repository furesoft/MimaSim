using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Compare
{
    public class NotCompareInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.CMPN;

        public bool Invoke(CPU cpu)
        {
            var accu = cpu.GetRegister(Registers.Accumulator);

            if (accu == 1)
            {
                cpu.SetRegister(Registers.Accumulator, (short)0);
            }
            else
            {
                cpu.SetRegister(Registers.Accumulator, (short)1);
            }

            return false;
        }
    }
}