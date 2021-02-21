using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Move
{
    public class MovMemRegInstructiion : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_REG;

        public bool Invoke(CPU cpu)
        {
            var registerFrom = cpu.FetchRegister();
            var registerTo = cpu.FetchRegister();
            var value = cpu.GetRegister(registerFrom);

            cpu.SetRegister(registerTo, value);

            return false;
        }
    }
}