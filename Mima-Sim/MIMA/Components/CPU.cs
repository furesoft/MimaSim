using MimaSim.Core;
using MimaSim.Messages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MimaSim.MIMA.Components
{
    public class CPU
    {
        public static CPU Instance = new CPU();

        public Register Accumulator = new Register("Accumulator");
        public ALU ALU;
        public Clock Clock = new Clock(1024);
        public ControlUnit ControlUnit = new ControlUnit();
        public Bus DataBus = new Bus();

        public Dictionary<OpCodes, IInstruction> Instructions = new Dictionary<OpCodes, IInstruction>();
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

            if (nextInstuctionAddress < Program.Length)
            {
                var instruction = Program[nextInstuctionAddress];

                SetRegister(Registers.IAR, (byte)(nextInstuctionAddress + 1));

                return instruction;
            }
            else
            {
                MessageBus.Current.SendMessage(new StopMessage());

                return (byte)OpCodes.NOP;
            }
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
            var regName = Enum.GetName<Registers>(reg);

            return RegisterMap.GetRegister(regName).GetValue();
        }

        public ushort GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        //Hack for Init all Fields
        public void Init()
        {
            InitInstructions();
        }

        public void SetRegister(Registers reg, ushort value)
        {
            RegisterMap.GetRegister(Enum.GetName(reg)).SetValue(value);
        }

        public void SetRegister(byte reg, ushort value)
        {
            SetRegister((Registers)reg, value);
        }

        public bool Step()
        {
            BusRegistry.ActivateBus("controlunit_iar");

            var instr = Fetch();
            return Step((OpCodes)instr);
        }

        public bool Step(OpCodes instruction)
        {
            if (Instructions.ContainsKey(instruction))
            {
                BusRegistry.DeactivateBus("controlunit_iar");

                return Instructions[instruction].Invoke(this);
            }
            else
            {
                throw new Exception("unknown opcode");
            }
        }

        private void InitInstructions()
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(_ => _.GetInterfaces().Contains(typeof(IInstruction)));
            foreach (var t in types)
            {
                var instance = (IInstruction)Activator.CreateInstance(t);

                Instructions.Add(instance.Instruction, instance);
            }
        }
    }
}