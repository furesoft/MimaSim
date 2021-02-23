using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();

            var address = cpu.Fetch();
            cpu.Memory.SetValue(address, cpu.GetRegister(register));

            return false;
        }
    }
}