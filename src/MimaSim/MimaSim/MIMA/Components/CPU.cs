using MimaSim.Core;
using MimaSim.Messages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MimaSim.MIMA.Components;

public class CPU
{
    public readonly static CPU Instance = new();

    public readonly Register Accumulator = new("Accumulator");
    public readonly ALU ALU;
    public readonly Clock Clock = new(250);
    public readonly ControlUnit ControlUnit = new();
    public readonly Bus DataBus = new();

    public readonly static Dictionary<OpCodes, IInstruction> Instructions = new();
    public readonly Memory Memory = new((int)Math.Pow(2, 8));
    public readonly Stack Stack;

    public readonly Register SAR = new("SAR");
    public readonly Register SDR = new("SDR");

    public readonly Display Display = new();

    public readonly Register X = new("X");

    public readonly Register Y = new("Y");

    public byte[]? Program { get; set; }

    public CPU()
    {
        ALU = new ALU(this);
        Stack = new Stack(this);

        ControlUnit.IAR.Bus.Subscribe(_ =>
        {
            BusRegistry.ActivateBus("controlunit_iar");
        });
        ControlUnit.SP.Bus.Subscribe(_ =>
        {
            BusRegistry.ActivateBus("controlunit_sp");
        });

        X.Bus.Subscribe(_ =>
        {
            BusRegistry.Activate("cu->x");
        });

        Y.Bus.Subscribe(_ =>
        {
            BusRegistry.Activate("cu->y");
        });

        Display.DC.Bus.Subscribe(_ =>
        {
            var isTextMode = CPU.Instance.ControlUnit.HasFlag(Flags.TextMode);

            if (isTextMode)
            {
                Instance.Display.Font.DrawChar();
            }
            else
            {
                Display.SetPixel();
            }
        });
    }

    static CPU()
    {
        var types = Assembly.GetCallingAssembly().GetTypes().Where(_ => _.GetInterfaces().Contains(typeof(IInstruction)));
        foreach (var t in types)
        {
            var instance = (IInstruction)Activator.CreateInstance(t);

            Instructions.Add(instance.OpCode, instance);
        }
    }

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

        return BitConverter.ToInt16([first, second], 0);
    }

    public Registers FetchRegister()
    {
        return (Registers)Fetch();
    }

    public short GetRegister(Registers reg)
    {
        var regName = Enum.GetName(reg);

        return RegisterMap.GetRegister(regName).GetValue();
    }

    public short GetRegister(byte reg)
    {
        return GetRegister((Registers)reg);
    }

    public void SetRegister(Registers reg, short value)
    {
        RegisterMap.GetRegister(Enum.GetName(reg)!).SetValue(value);
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
}