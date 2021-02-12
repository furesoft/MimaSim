using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class NotInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.NOT;
        public string Mnemonic => "not";
        public InstructionTypeSizes Size => InstructionTypeSizes.SingleReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var registerValue = cpu.GetRegister(r1);

            var res = (~registerValue) & 0xffff;
            cpu.SetRegister(Registers.Accumulator, (ushort)res);

            return false;
        }
    }
}