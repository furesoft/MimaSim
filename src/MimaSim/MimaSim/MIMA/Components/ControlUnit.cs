namespace MimaSim.MIMA.Components;

public class ControlUnit
{
    public Bus AccuBus = new();
    public Register FLAG = new("FLAG");
    public Register IAR = new("IAR");
    public Register SP = new("SP", 49);
}