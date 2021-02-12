namespace MimaSim.Messages
{
    public class MemoryCellChangedMessage
    {
        public ushort Address { get; set; }
        public ushort Value { get; set; }
    }
}