using System;

namespace MimaSim.MIMA
{
    public class Bus
    {
        private event Action<object> DataRecieved;

        public void Subscribe(Action<object> callback)
        {
            DataRecieved += callback;
        }

        public void Send(object data)
        {
            DataRecieved?.Invoke(data);
        }

        public void Send(object data, Bus bus)
        {
            bus.Send(data);
        }

        public void Connect(Bus bus)
        {
            Subscribe(bus.DataRecieved);
            bus.Subscribe(DataRecieved);
        }
    }
}