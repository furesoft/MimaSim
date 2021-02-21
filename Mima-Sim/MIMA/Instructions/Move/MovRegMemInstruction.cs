using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovMemMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_MEM_MEM;

        public bool Invoke(CPU cpu)
        {
            var fromAddress = cpu.Fetch16();
            var value = cpu.Memory.GetValue(fromAddress);

            var toAddress = cpu.Fetch16();
            cpu.Memory.SetValue(toAddress, value);

            return false;
        }
    }

    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();

            var address = cpu.Fetch16();
            cpu.Memory.SetValue(address, cpu.GetRegister(register));

            return false;
        }
    }
}