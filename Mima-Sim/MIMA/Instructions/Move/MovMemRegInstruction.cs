using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovMemRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_MEM_REG;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();
            var registerTo = cpu.FetchRegister();
            var value = cpu.Memory.GetValue(address);

            cpu.SetRegister(registerTo, value);

            return false;
        }
    }
}