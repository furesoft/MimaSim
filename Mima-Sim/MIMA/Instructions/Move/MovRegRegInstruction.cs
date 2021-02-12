using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_REG;
        public string Mnemonic => "move";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;

        public bool Invoke(CPU cpu)
        {
            var fromReg = cpu.Fetch();
            var toReg = cpu.Fetch();

            var fromValue = cpu.GetRegister(fromReg);

            cpu.SetRegister(toReg, fromValue);

            return false;
        }
    }
}