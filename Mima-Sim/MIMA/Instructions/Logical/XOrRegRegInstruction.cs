﻿using MimaSim.Core;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA.Instructions.Logical
{
    public class XOrRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.XOR;

        public bool Invoke(CPU cpu)
        {
            var registerValue1 = cpu.GetRegister(Registers.X);
            var registerValue2 = cpu.GetRegister(Registers.Y);

            var res = registerValue1 ^ registerValue2;
            cpu.SetRegister(Registers.Accumulator, (byte)res);

            return false;
        }
    }
}