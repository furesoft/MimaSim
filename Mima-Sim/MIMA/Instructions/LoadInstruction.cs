using MimaSim.Core;
using MimaSim.MIMA.Components;
using System.Threading;
using System.Threading.Tasks;

namespace MimaSim.MIMA.Instructions
{
    public class LoadInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.LOAD;

        public bool Invoke(CPU cpu)
        {
            BusRegistry.GetBusMap("cu->accu").Activate();
            cpu.SetRegister(Registers.Accumulator, cpu.Fetch16());

            return true;
        }
    }
}