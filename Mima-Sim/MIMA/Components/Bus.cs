using System;

namespace MimaSim.MIMA.Components
{
    public class Bus
    {
        private event Action<object> DataRecieved;

        public void Connect(Bus bus)
        {
            Subscribe(bus.DataRecieved);
            bus.Subscribe(DataRecieved);
        }

        public void Send(object data)
        {
            DataRecieved?.Invoke(data);
        }

        public void Send(object data, Bus bus)
        {
            bus.Send(data);
        }

        public void Subscribe(Action<object> callback)
        {
            DataRecieved += callback;
        }
    }
}