using MimaSim.Core;
using MimaSim.Messages;
using MimaSim.MIMA.Components;
using ReactiveUI;

namespace MimaSim.MIMA.Instructions
{
    public class ExitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Exit;
        public string Mnemonic => "exit";

        public InstructionTypeSizes Size => InstructionTypeSizes.NoArg;

        public bool Invoke(CPU cpu)
        {
            cpu.Clock.Stop();
            MessageBus.Current.SendMessage(new StopMessage());

            return false;
        }
    }
}