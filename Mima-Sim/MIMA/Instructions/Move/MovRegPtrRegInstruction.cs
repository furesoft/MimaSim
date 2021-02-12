using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegPtrRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_PTR_REG;
        public string Mnemonic => "move";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegPtrReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var ptr = cpu.GetRegister(r1);
            var value = cpu.Memory.GetValue(ptr);

            cpu.SetRegister(r2, value);

            return false;
        }
    }
}