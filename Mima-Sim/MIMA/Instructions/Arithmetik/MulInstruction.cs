using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class MulInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Mul;

        public string Mnemonic => "mul";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.GetRegister(Registers.X);
            var r2 = cpu.GetRegister(Registers.Y);

            cpu.SetRegister(Registers.Accumulator, (ushort)(r1 * r2));

            return false;
        }
    }
}