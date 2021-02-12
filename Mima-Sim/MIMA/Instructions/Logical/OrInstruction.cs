using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class OrInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Or;
        public string Mnemonic => "or";

        public bool Invoke(CPU cpu)
        {
            var registerValue1 = cpu.GetRegister(Registers.X);
            var registerValue2 = cpu.GetRegister(Registers.Y);

            var res = registerValue1 | registerValue2;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}