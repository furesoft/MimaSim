using System;
using System.Timers;

namespace MimaSim.MIMA.Components
{
    public class Clock
    {
        private short _frequency;

        private Timer _timer;

        public Clock(short frequency)
        {
            _frequency = frequency;
            _timer = new Timer(_frequency);
            _timer.Elapsed += _timer_Elapsed;
        }

        public event Action<object> FrequencyChanged;

        public event Action Tick;

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
            Tick?.Invoke();
        }
    }
}