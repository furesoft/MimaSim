namespace MimaSim.Messages
{
    public class MemoryCellChangedMessage
    {
        public byte Address { get; set; }
        public byte Value { get; set; }
    }
}