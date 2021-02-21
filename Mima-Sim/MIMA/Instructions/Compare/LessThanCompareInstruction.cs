﻿using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Compare
{
    public class LessThanCompareInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.CMPLT;

        public bool Invoke(CPU cpu)
        {
            var left = cpu.GetRegister(Registers.X);
            var right = cpu.GetRegister(Registers.Y);

            var value = left < right;

            cpu.SetRegister(Registers.Accumulator, value ? 1 : 0);

            return false;
        }
    }
}