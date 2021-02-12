using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var registerFrom = cpu.FetchRegister();
            var address = cpu.Fetch16();
            var value = cpu.GetRegister(registerFrom);

            cpu.Memory.SetValue(address, value);

            return false;
        }
    }
}