using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public bool Invoke(CPU cpu)
        {
            BusRegistry.GetBusMap("cu->adr").Activate();
            var register = cpu.FetchRegister();

            BusRegistry.GetBusMap("cu->data").Activate();

            var address = cpu.Fetch16();
            BusRegistry.GetBusMap("cu->accu").Activate();
            cpu.Memory.SetValue(address, cpu.GetRegister(register));

            return false;
        }
    }
}