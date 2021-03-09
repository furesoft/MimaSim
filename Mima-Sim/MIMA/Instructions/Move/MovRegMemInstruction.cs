using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public bool Invoke(CPU cpu)
        {
            BusRegistry.Activate("cu->accu");
            BusRegistry.Activate("cu->adr");
            BusRegistry.Activate("cu->data");

            var register = cpu.FetchRegister();

            var address = cpu.Fetch16();

            cpu.Memory.SetValue(address, cpu.GetRegister(register));

            return false;
        }
    }
}