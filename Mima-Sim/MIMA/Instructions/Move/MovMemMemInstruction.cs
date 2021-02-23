using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovMemMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_MEM_MEM;

        public bool Invoke(CPU cpu)
        {
            var fromAddress = cpu.Fetch();
            var value = cpu.Memory.GetValue(fromAddress);

            var toAddress = cpu.Fetch();
            cpu.Memory.SetValue(toAddress, value);

            return false;
        }
    }
}