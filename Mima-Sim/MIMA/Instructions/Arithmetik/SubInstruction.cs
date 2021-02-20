﻿using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik
{
    public class SubInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.SUB;

        public string Mnemonic => "sub";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.GetRegister(Registers.X);
            var r2 = cpu.GetRegister(Registers.Y);

            cpu.SetRegister(Registers.Accumulator, (ushort)(r1 - r2));

            return false;
        }
    }
}