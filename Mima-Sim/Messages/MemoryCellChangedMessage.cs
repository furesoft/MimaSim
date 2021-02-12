using MimaSim.MIMA;

namespace MimaSim.Messages
{
    public class MemoryCellChangedMessage
    {
        public TinyInt Address { get; set; }
        public TinyInt Value { get; set; }
    }
}