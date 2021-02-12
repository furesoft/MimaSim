using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik.Mul
{
    public class MulLitRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MUL_LIT_REG;

        public string Mnemonic => "mul";
        public InstructionTypeSizes Size => InstructionTypeSizes.LitReg;

        public bool Invoke(CPU cpu)
        {
            var literal = cpu.Fetch16();
            var r1 = cpu.FetchRegister();
            var registerValue = cpu.GetRegister(r1);

            cpu.SetRegister(Registers.Accumulator, (ushort)(literal * registerValue));

            return false;
        }
    }
}