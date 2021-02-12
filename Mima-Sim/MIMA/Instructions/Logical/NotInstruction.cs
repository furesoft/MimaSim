using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class NotInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.NOT;
        public string Mnemonic => "not";

        public bool Invoke(CPU cpu)
        {
            var registerValue = cpu.GetRegister(Registers.X);

            var res = (~registerValue) & 0xffff;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}