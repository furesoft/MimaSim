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

    private Dictionary<SysCall, Action> _syscalls = new();

    public void AddSysCall(SysCall syscall, Action action)
    {
        _syscalls.TryAdd(syscall, action);
    }

    public void InvokeSysCall(SysCall syscall)
    {
        _syscalls.TryGetValue(syscall, out Action? action);

        action!();
    }

    public bool HasFlag(Flags flag)
    {
        return (FLAG.GetValueWithoutNotification() & (1 << (short)flag)) != 0;
    }
}