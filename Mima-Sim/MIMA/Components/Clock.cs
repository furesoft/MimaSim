﻿using System;
using System.Timers;

namespace MimaSim.MIMA.Components
{
    public class Clock
    {
        private short _frequency;

        public event Action<object> FrequencyChanged;

        public Clock(short frequency)
        {
            _frequency = frequency;
            _timer = new Timer(_frequency);
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke();
        }

        public void SetFrequency(short frequency)
        {
            _frequency = frequency;
            _timer.Interval = frequency;
            FrequencyChanged?.Invoke(frequency);
        }

        public short Frequency => _frequency;

        public event Action Tick;

        private Timer _timer;
    }
}