﻿using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.VM;

namespace MimaSim.MIMA.Instructions.Arithmetik.Add
{
    public class AddRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.ADD_REG_REG;

        public string Mnemonic => "add";
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            cpu.SetRegister(Registers.Accumulator, (ushort)(registerValue1 + registerValue2));

            return false;
        }
    }
}