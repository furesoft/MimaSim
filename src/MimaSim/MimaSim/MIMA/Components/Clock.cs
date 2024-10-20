﻿using MimaSim.Core;
using System;
using System.Timers;

namespace MimaSim.MIMA.Components;

public class Clock
{
    private readonly Timer _timer;
    private short _frequency;

    public Clock(short frequency)
    {
        _frequency = frequency;
        _timer = new Timer(_frequency);
        _timer.Elapsed += _timer_Elapsed;
    }

    public event Action<object> FrequencyChanged;

    public short Frequency => _frequency;

    public void SetFrequency(short frequency)
    {
        _frequency = frequency;
        _timer.Interval = frequency;
        FrequencyChanged?.Invoke(frequency);
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        CPU.Instance.Step();

        System.Threading.Thread.Sleep(10);

        BusRegistry.DeactivateAll();
    }
}