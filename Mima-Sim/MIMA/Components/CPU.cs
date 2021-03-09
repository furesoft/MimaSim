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
        public Clock Clock = new Clock(250);
        public ControlUnit ControlUnit = new ControlUnit();
        public Bus DataBus = new Bus();

        public Dictionary<OpCodes, IInstruction> Instructions = new Dictionary<OpCodes, IInstruction>();
        public Memory Memory = new Memory((int)Math.Pow(2, 24));

        public Register SAR = new Register("SAR");
        public Register SDR = new Register("SDR");

        public Register X = new Register("X");

        public Register Y = new Register("Y");

        public CPU()
        {
            ALU = new ALU(this);

            ControlUnit.IAR.Bus.Subscribe(_ =>
            {
                BusRegistry.ActivateBus("controlunit_iar");
            });

            X.Bus.Subscribe(_ =>
            {
                BusRegistry.Activate("cu->x");
            });

            Y.Bus.Subscribe(_ =>
            {
                BusRegistry.Activate("cu->y");
            });
        }

        public byte[] Program { get; set; }

        public byte Fetch()
        {
            var nextInstuctionAddress = GetRegister(Registers.IAR);

            if (nextInstuctionAddress < Program.Length)
            {
                var instruction = Program[nextInstuctionAddress];

                SetRegister(Registers.IAR, (short)(nextInstuctionAddress + 1));

                return instruction;
            }
            else
            {
                MessageBus.Current.SendMessage(new StopMessage());

                return (byte)OpCodes.NOP;
            }
        }

        public short Fetch16()
        {
            var first = Fetch();
            var second = Fetch();

            return BitConverter.ToInt16(new byte[] { first, second }, 0);
        }

        public Registers FetchRegister()
        {
            return (Registers)Fetch();
        }

        public short GetRegister(Registers reg)
        {
            var regName = Enum.GetName<Registers>(reg);

            return RegisterMap.GetRegister(regName).GetValue();
        }

        public short GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        //Hack for Init all Fields
        public void Init()
        {
            InitInstructions();
        }

        public void SetRegister(Registers reg, short value)
        {
            RegisterMap.GetRegister(Enum.GetName(reg)).SetValue(value);
        }

        public void SetRegister(byte reg, short value)
        {
            SetRegister((Registers)reg, value);
        }

        public bool Step()
        {
            var instr = Fetch();
            var result = Step((OpCodes)instr);

            return result;
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