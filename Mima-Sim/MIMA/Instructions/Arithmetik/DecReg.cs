﻿using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class DecReg : IInstruction
    {
        public OpCodes Instruction => OpCodes.DEC;

        public bool Invoke(CPU cpu)
        {
            var oldValue = cpu.GetRegister(Registers.Accumulator);
            var newValue = oldValue - 1;

            cpu.SetRegister(Registers.Accumulator, (byte)newValue);

            return false;
        }
    }
}