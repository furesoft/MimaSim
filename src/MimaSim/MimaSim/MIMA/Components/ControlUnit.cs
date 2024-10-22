namespace MimaSim.MIMA.Components;

public class ControlUnit
{
    public Bus AccuBus = new();
    public Register FLAG = new("FLAG");
    public Register IAR = new("IAR");
    public Register SP = new("SP", 49);

    public bool HasFlag(Flags flag)
    {
        return (FLAG.GetValueWithoutNotification() & (1 << (short)flag)) != 0;
    }
}