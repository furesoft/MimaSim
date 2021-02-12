using MimaSim.Core;
using MimaSim.MIMA.VM;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Components
{
    public class CPU
    {
        public static CPU Instance = new CPU();

        public Register Accumulator = new Register("Accumulator", 42);
        public ALU ALU;
        public Clock Clock = new Clock(1024);
        public ControlUnit ControlUnit = new ControlUnit();
        public Bus DataBus = new Bus();

        public Dictionary<OpCodes, IInstruction> Instructions;
        public Memory Memory = new Memory((int)Math.Pow(2, 24));
        public Register One = new Register("One", 1);
        public Register SAR = new Register("SAR");
        public Register SDR = new Register("SDR");

        public Register X = new Register("X");

        public Register Y = new Register("Y");

        public Register Z = new Register("Z");

        public CPU()
        {
            ALU = new ALU(this);
        }

        public byte[] Program { get; internal set; }

        public byte Fetch()
        {
            var nextInstuctionAddress = GetRegister(Registers.IAR);
            var instruction = Program[nextInstuctionAddress];
            SetRegister(Registers.IAR, (ushort)(nextInstuctionAddress + 1));

            return instruction;
        }

        public ushort Fetch16()
        {
            var first = Fetch();
            var second = Fetch();

            return BitConverter.ToUInt16(new byte[] { first, second }, 0);
        }

        public Registers FetchRegister()
        {
            return (Registers)Fetch();
        }

        public ushort GetRegister(Registers reg)
        {
            return RegisterMap.GetRegister(Enum.GetName(typeof(Registers), reg)).GetValue();
        }

        public ushort GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        //Hack for Init all Fields
        public void Init()
        {
        }

        public void SetRegister(Registers reg, ushort value)
        {
            RegisterMap.GetRegister(Enum.GetName(typeof(Registers), reg)).SetValue(value);
        }

        public void SetRegister(byte reg, ushort value)
        {
            SetRegister((Registers)reg, value);
        }

        public bool Step()
        {
            var instr = Fetch();
            return Step((OpCodes)instr);
        }

        public bool Step(OpCodes instruction)
        {
            if (Instructions.ContainsKey(instruction))
            {
                return Instructions[instruction].Invoke(this);
            }
            else
            {
                throw new Exception("unknown opcode");
            }
        }
    }
}