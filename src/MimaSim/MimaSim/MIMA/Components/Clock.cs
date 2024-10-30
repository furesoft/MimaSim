using System;
using System.Timers;
using MimaSim.Core;

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

        Stoped += BusRegistry.DeactivateAll;
    }

    public event Action<object> FrequencyChanged;
    public event Action Stoped;

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
        Stoped?.Invoke();
    }

    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        BusRegistry.DeactivateAll();

        CPU.Instance.Step();
    }
}