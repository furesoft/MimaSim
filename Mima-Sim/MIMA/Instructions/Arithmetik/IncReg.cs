using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class IncReg : IInstruction
    {
        public OpCodes Instruction => OpCodes.INC_REG;

        public string Mnemonic => "inc";

        public bool Invoke(CPU cpu)
        {
            var oldValue = cpu.GetRegister(Registers.Accumulator);
            var newValue = oldValue + 1;

            cpu.SetRegister(Registers.Accumulator, (ushort)newValue);

            return false;
        }
    }
}