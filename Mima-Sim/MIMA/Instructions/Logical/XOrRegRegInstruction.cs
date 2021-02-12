using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class XOrRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Xor;
        public string Mnemonic => "xor";

        public bool Invoke(CPU cpu)
        {
            var registerValue1 = cpu.GetRegister(Registers.X);
            var registerValue2 = cpu.GetRegister(Registers.Y);

            var res = registerValue1 ^ registerValue2;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}