using ReactiveUI;

namespace MimaSim.Models;

public class MemoryCellModel(short address, short value) : ReactiveObject
{
    public short Address { get; } = address;
    public short Value { get; set; } = value;
}