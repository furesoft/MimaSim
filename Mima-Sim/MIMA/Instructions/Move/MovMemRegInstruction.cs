using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovMemRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_MEM_REG;

        public bool Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();
            var value = cpu.Memory.GetValue(address);
            var register = cpu.FetchRegister();

            cpu.SetRegister(register, value);

            return false;
        }
    }
}