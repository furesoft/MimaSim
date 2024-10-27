using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Components;

public class ControlUnit
{
    public Bus AccuBus = new();
    public Register FLAG = new("FLAG");
    public Register IAR = new("IAR");
    public Register SP = new("SP", 49);
    public Register SCR = new("SCR");
    public Action? _interrupt;

    private Dictionary<SysCall, Action> _syscalls = new();

    public void Interrupt(Action? action)
    {
        _interrupt += action;
    }

    public void InvokeInterrupt()
    {
        _interrupt?.Invoke();
        _interrupt = null;
    }

    public void AddSysCall(SysCall syscall, Action action)
    {
        _syscalls.TryAdd(syscall, action);
    }

    public void InvokeSysCall(SysCall syscall)
    {
        if (_syscalls.TryGetValue(syscall, out var action))
        {
            action();
            return;
        }

        SetError(ErrorCodes.SysCallNotFound);
    }

    public bool HasFlag(Flags flag)
    {
        return (FLAG.GetValueWithoutNotification() & (1 << (short)flag)) != 0;
    }

    public void SetFlag(Flags flag)
    {
        var current = FLAG.GetValueWithoutNotification();

        current = (short)((ushort)current | 1 << (short)flag);

        FLAG.SetValue(current);
    }

    public void SetError(ErrorCodes code)
    {
        CPU.Instance.Accumulator.SetValue((short)code);

        SetFlag(Flags.Trap);

        CPU.Instance.Clock.Stop();
    }
}